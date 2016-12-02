<?php 

class WebApp
 {	
	protected $DBH;	
	protected $tpl;
	protected $msgArray = array();
	protected $settings = array();
	protected $constants = array();
	
	public function __construct() 
	{	
		// zarejdame nastroikite ot config.php
		require_once('config.php');
		$this->settings = $settings;	
		
		// zarejdame syobshteniata za greshki ot errorMsgs.php
		require_once('errorMsgs.php');
		$this->msgArray = $msgArray;
		
		// === Inicializirame template engine-a
		$this->tpl = new Template($this->settings['pathToTemplates']);
		$this->tpl->set('JavaScript', '');
		
		// === Otvarqme wryzkata kym BD
		@$this->DBH = new mysqli($host, $user, $password, $db_name);
		if (mysqli_connect_error())
		{
			$this->message('DB_unable_to_connect');
		}
		
		$this->DBH->query("SET NAMES cp1251");
		
		// === Inicializirame sesiata
		session_save_path('./Session');
		session_start();
	}
	
	// message - just display message within the "content" area
	public function message($msg, $directInput = false)
	{
		if ($directInput) 
		{
			$this->tpl->set('content', $msg);
		} 
		else 
		{
			$this->tpl->set('content', $this->msgArray[$msg]);
		}
		
		print $this->tpl->fetch('_main_template.html');
		
		$this->__destruct();
	} // end of message
	
	public function getLoginScreen($errorMsg = '') {
		
	if (!empty($errorMsg)) {
		$this->tpl->set('errorMsg', $this->msgArray[$errorMsg]);
	} else {
		$this->tpl->set('errorMsg', '');
	}
	
	$this->tpl->set('content', $this->tpl->fetch('_login_screen.html'));
	print $this->tpl->fetch('_main_template.html');
}

// login
public function login($initUser, $initPass) {
	
	// Добавят се escape символи в потребителското име
	// ако е необходимо.
	$initUser = $this->DBH->real_escape_string(stripslashes($initUser));
	// Да се генерира мд5 хеша на паролата. Използвайте и променливата $secretKey от config.php
	$initPass = md5($this->settings['secretKey'].$initPass);
	// SQL заявката за проверка
 	$zaiavka = "SELECT username, userGroup FROM potrebiteli WHERE password = '$initPass' and username='$initUser'";

	// Изпълнение на заявката
	if (!$result = $this->DBH->query($zaiavka)) 
	{
		print $this->DBH->error;
		$this->__destruct();
	}
	
	// Проверка дали има потребител с такива име и парола
	if ($row = $result->fetch_assoc()) 
	{
		
		// Да, има потребител с такива име и парола
		
		// Обновете lastLogin атрибута
		
		// Генерирайте нов сесиен идентификатор
		
		// Съхранете в сесията следнтие данни:
		// - потребителско име  _SESSION - vgradeno v PHP ; vliza susu faila vuv sesiqta kudeto se suzdava
		$_SESSION['user'] = $row['username'];
		// - код на потребителската група
		$_SESSION['userGroup'] = $row['userGroup'];
		// - времевия маркер кога последно е бил активен потребителя
		$_SESSION['lastActive'] = time();
		// - мд5 хеша на идентификационния стринг на браузъра
		$_SESSION['idhash'] = md5($_SERVER['HTTP_USER_AGENT']);
		
		// return <кода на потребителската група>;
		return $_SESSION['userGroup'];
	} 
	else 
	{
		// Невалидни потребителско име и/или парола
		return 0;
	}
} // end of login();
	
	
public function isUserLogged()
 {
	
	// Проверка дали има "логнат" потребител,
	// т.е. в сесията дали има потребителско име
	if (empty($_SESSION['user'])) 
	{
		return 0;
	}
	
	// Проверка дали сесията е изтекла. Използвайте и променливата $session_timeout от config.php
	if (time() > $_SESSION['lastActive'] + $this->settings['session_timeout']) 
	{
		return 0;
	}
	
	// Проверка дали сесията не е била открадната (на базата на мд5 хеша идентификационния стринг на браузъра)
	if ($_SESSION['idhash'] != md5($_SERVER['HTTP_USER_AGENT'])) 
	{
		return 0;
	}
		
	return $_SESSION['userGroup'];
}
	
// void logout()
public function logout() 
{	
	// Изтривате от сесията всички идентификационни маркери, записани от login();
	unset($_SESSION['user']);		
	unset($_SESSION['userGroup']);		
	unset($_SESSION['lastActive']);
	unset($_SESSION['idhash']);		
}

public function setInformationRow()
{
	if($this->isUserLogged())
	{
		
		$this->tpl->set("user",$_SESSION['user']);
		$this->tpl->set("informationRow",$this->tpl->fetch('_logged_in_as.html'));
	}
	else
	{
		$this->tpl->set("informationRow","");
	}
}	
	
	public function __destruct()
	{
		@$this->DBH->close();
		unset($this->DBH);
		unset($this->tpl);
		unset($this->msgArray);
		unset($this->settings);
		exit;
	}
	
} // end of class


?>