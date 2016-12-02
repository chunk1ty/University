<?php

require_once('./modules/template.php');
require_once('./modules/webApp.php');

if (!empty($_REQUEST['cmd'])) 
{
	$cmd = $_REQUEST['cmd'];
} 
else 
{
	$cmd = 'na_mainMenu';
}

// opredeliane koi modul (class) da se zaredi
$cmdArr = split('_', $cmd);
//print_r(array_values($cmdArr));

if (strtolower($cmdArr[0]) == 'ch')
{
	require_once('./modules/adm-coaches.php');
	$app = new CoachManagement();
} 
elseif (strtolower($cmdArr[0]) == 'plr') 
{
	require_once('./modules/adm-players.php');
	$app = new PlayerManagement();
}
else 
{
	$app = new WebApp();
}

$app->setInformationRow();


if($cmd != 'user_login')
{
	$_SESSION['previosPage'] = $_SERVER['QUERY_STRING'];
}

if ($app->isUserLogged())
{
	if (!empty($cmdArr[1])) 
	{
		if ($cmdArr[1] == 'addScreen') 
		{
			$app->addScreen();
		}
		elseif ($cmdArr[1] == 'addScreenPlrRating') 
		{
			$app->addScreenPlrRating();
		}
		elseif ($cmdArr[1] == 'save')
		{		
			$app->save($_POST['data'], @$_POST['id']);
		} 
		elseif ($cmdArr[1] == 'browse') 
		{
			$app->browse(@$_GET['search']);
		}		
		elseif ($cmdArr[1] == 'editScreen') 
		{
			$app->editScreen($_GET['ID']);
		} 
		elseif ($cmdArr[1] == 'delete') 
		{
			$app->delete($_GET['id']);
		}
		elseif ($cmdArr[1] == 'report')
		{
						//@ -> ignore if exeption exist
			$app->report(@$_GET['ID']);
		}
		elseif ($cmdArr[1] == 'logout')
		{
			$app->logout();
			header("Location: index.php?cmd=stud_welcome");
		}	
		else 
		{
			// izvejdane na glavnoto menu
			//$app = new specManagement();
			$app->message('<h1><center><b> Welcome ! </b></center</h1>', true);
		}
	}
	else 
	{
		$app->message('<div style="text-align:center; font-weight:bold"> Невалиден синтаксис на cmd! </div>', true);
	}
}
else
{
	if ($cmdArr[1] == 'login')
	{
		if($app -> login($_POST['username'],$_POST['password']))
		{
			header("Location: index.php?".$_SESSION['previosPage']);
		}
		else
		{
			$app->getLoginScreen('login_invalid');
		}
	} 
	elseif ($cmdArr[1] == 'browse') 
	{
		$app->browse(@$_GET['search']);
	} 
	else 
	{
		$app->getLoginScreen();
	}
}

	
?>