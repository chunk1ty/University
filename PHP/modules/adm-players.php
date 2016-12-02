<?php

// Modul student management

class PlayerManagement extends WebApp 
{
	
public function __construct() 
{
	parent::__construct();
}

public function browse($search = '', $msg = '') 
{	
	// ako $search ne e prazna, znachi potrebiteliat e vyvel neshto po koeto da tyrsi
	if (!empty($search)) 
	{
		$WHERE_SEARCH = "((ID LIKE '%$search%') OR (FirstName LIKE '%$search%') OR (LastName LIKE '%$search%'))";
		$this->tpl->set('search', $search);
	}
	else 
	{
		// a ako e prazna ne se tyrsi po nishto i zatova sled where ima (1), koeto vse edno oznachava che niama where
		$WHERE_SEARCH = '(1)';
		$this->tpl->set('search', '');
	}
	
	$query = 'SELECT ID, FirstName, LastName, Nationality, Height , Weight FROM Players WHERE '.$WHERE_SEARCH;
	
	// izpylniavame zaiavkata
	if (!$result = $this->DBH->query($query)) 
	{
		print $this->DBH->error;
		$this->__destruct();
	}
	
	$this->tpl->set('numberOfPlayers', $result->num_rows);
	$temp = '';
	$i = 0;
	
	while ($row = $result->fetch_assoc()) 
	{		
		foreach($row as $key => $value)
		{
			$this->tpl->set($key, $value);			
		}
				
		if ($i % 2) 
		{
			$this->tpl->set('bgColor', '#f985bf');
		} 
		else 
		{
			$this->tpl->set('bgColor', '#E0E0E0');
		}
		$i++;
		
		// obrabotva i izvlicha obrabotenia HTML kod za tekushtia red
		$temp = $temp . $this->tpl->fetch('_browse_players_row.html');
	}	
	
	// mnojestvoto ot vsichki redove se namira v $temp i 
	// triabva da go vmyknem v HTML koda na _browse_specialities.html
	$this->tpl->set('player', $temp);
		
	// ako niama namerena specialnost sled tyrseneto ili niama nishto v BD
	// se izvejda syosbhtenie che niama takava specialnost
	// to se vzema ot errorMsgs.php faila razpolojen v glavnata direktoria
	if (0 == $i) 
	{		
		$this->tpl->set('player', $this->msgArray['br_no_such_player']);
	}
	
	// ako ima syobshtenie prateno ot niakoi drug metod - tezi za redaktirane ili iztrivane
	// izvejdame syobshtenieto 
	if (!empty($msg)) 
	{
		$this->tpl->set('msg', $this->msgArray[$msg]);
	} 
	else 
	{
		$this->tpl->set('msg', '');
	}
		
	//tozi javascript sydyrja funkcia, koito pita potrebitelia 
	// siguren li e che iska da iztrie tazi specialnost i vsichki studenti koito se obuchavat po neia
	$this->tpl->set('JavaScript', $this->tpl->fetch('javascript.js'));
	
	// content e etikatena na dinamichnata oblast v osnovnia shablon (tozi s menuto i zaglavnata lenta)
	$this->tpl->set('content', $this->tpl->fetch('_browse_players.html'));
	print $this->tpl->fetch('_main_template.html');
} // end of browse();

						// id e prazno zashtoto pri purvonachalno izvikvane potrebitelq ne e vuvel id
public function report($id = '')
{
	//tpl-> identifikator kum koito se obrushtam kum interfeisa
	$this->tpl->set('section_2','');
	$this->tpl->set('section_3','');
	$this->tpl->set('ID',$id);
	
	// Section 2
	if (!empty($id)) 
	{		
		// извличате съответните данни за съответната секция
		$data = " SELECT ID,FirstName,LastName,Nationality,Height,Weight,Name FROM Players pl,Position po
				  WHERE pl.PlayerPosition = po.PositionId
				  and pl.ID = '$id'"; 
				  
		if (!$result = $this->DBH->query($data)) 
		{
			print $this->DBH->error;
			$this->__destruct();
		}
			// и обработвате HTML шаблоните
		if ($row = $result->fetch_assoc()) 
		{		
			foreach($row as $key => $value)
			{
				$this->tpl->set($key, $value);
			}				
			
			$this->tpl->set('section_2',$this->tpl->fetch('_report_player_section2.html'));			
		}
		else
		{			
			$this->tpl->set('section_2',$this->msgArray['no_such_player']);
		}
	} 
	
	// Section 3
	if (!empty($id)) 
	{	
		 // извличате съответните данни за съответната секция
		 $data = " SELECT * FROM Coaches c,Rating r, Teams t
				   WHERE c.ID = r.CoachID AND r.TeamID = t.TeamId
			       and r.PlayerID = '$id'"; 
			  
		 if (!$result = $this->DBH->query($data)) 
		 {
			print $this->DBH->error;
			$this->__destruct();
		 }
		 // и обработвате HTML шаблоните
		
		$currentData = '';
		$i = 0;
		 while ($row = $result->fetch_assoc()) 
		 {		
			foreach($row as $key => $value)
			{
				$this->tpl->set($key, $value);
			}
			
			if($i % 2 == 0)
			{
				$this->tpl->set('bgColor', '#f985bf');
			}
			else
			{
				$this->tpl->set('bgColor', '#e0e0e0');
			}
			
			$i++;
			
			$currentData .= $this->tpl->fetch('_report_player_section3_row.html');			
		 }
		 
		 $this->tpl->set('plrRating', $currentData);
		 $this->tpl->set('section_3',$this->tpl->fetch('_report_player_section3.html'));
	} 

	$this->tpl->set('content', $this->tpl->fetch('_report_player_section1.html'));
	print $this->tpl->fetch('_main_template.html');
} 

public function addScreen($data = '', $errorMessage = '', $id = '')
{		
	if (!empty($data)) 
	{
	// ако масивът не е празен, то данните 
	// въведени от потребителя ги изпращаме 
	// към value атрибутите за да се 
	// визуализират в текстовите полета
		foreach ($data as $k => $v) 
		{
			$this->tpl->set($k, $v);
		}	
	} 
	else
	{
		// ако обаче масивът е празен то value 
		// атрибутите трябва да останат празни
		// т.е. да не се визуализира нищо в текстовите полета
		$this->tpl->set('ID','');
		$this->tpl->set('FirstName', '');
		$this->tpl->set('LastName', '');
		$this->tpl->set('Nationality', '');
		$this->tpl->set('Height', '');
		$this->tpl->set('Weight', '');
	}		
	
	//Position
	$tempData = '';
	$query = 'SELECT * FROM Position';
	
	if ($result = $this->DBH->query($query)) 
	{
		while ($row = $result->fetch_assoc())
		{				
			$this->tpl->set('opValue', $row['PositionId']);
			$this->tpl->set('opTitle', $row['name']);
			
			if (!empty($data['PlayerPosition']) and ($data['PlayerPosition'] == $row['PositionId'])) 
			{
				$this->tpl->set('opSelected', ' selected');
			} 
			else
			{
				$this->tpl->set('opSelected', '');
			}
			$tempData .= $this->tpl->fetch('_select_box.html');
		}		
		$this->tpl->set('PlayerPosition', $tempData);		
	}
	else
	{
		//todo thow exception
	}	
	
	if (empty($errorMessage))
	{
		$this->tpl->set('errorMsg', '');
	}
	else
	{
		$this->tpl->set('errorMsg', $this->msgArray[$errorMessage]);
	}	
	
	$this->tpl->set('sid',$id);
	$this->tpl->set('content',$this->tpl->fetch('_addEdit_player_screen.html'));
	print $this->tpl->fetch('_main_template.html');
}

public function editScreen($id)
{	
	$query = "SELECT * FROM Players WHERE ID = '$id'";
	$result = $this->DBH->query($query);
	
	if ($row = $result->fetch_assoc())
	{
		// ако има данни за студента със зададен Ф.Н.
		// ги предаваме към метода
		$this->addScreen($row, '', $id);		
	} 
	else 
	{
		// ако няма данни за този студент, явно се
		// опиваме да редактираме несъществуващ студент
		// и трябва да се изведе някакво съобщение за грешка.
		// най-подходящо е да пренасочим потребителя
		// към екрана за преглед на
		// студенти и там да изведем съобщението за грешка.
		$this->browse('', 'edit_stud_non_existing');
		$this->__destruct();
	}
}
public function save($data,$id = '')
{	
	$insertFields = '';	
	$insertValues = '';
	$updateVariable = '';
	// value - stoinostya koqto e vuvedena ot potrebitelq
	// key - indeksa v bazata	
	foreach($data as $key => $value) 
	{
		// Проверка дали текущо обработваното поле е празно.
		if (empty($value)) 
		{			
			$this->addScreen($data, 'plr_'.$key.'_empty');
			exit;
		}
		// Друга обработка на данните (ако е необходима).
		// Долните редове генерират:
		// списък на атрибутите, разделени със запетая.
		// key se zamestva s neinata stoinost
		
		$insertFields .= ", $key";		
		// списък на стойностите, разделени със запетая.
		$insertValues .= ", '" . $this->DBH->real_escape_string(stripslashes($value))."'";
		
		if ($key != 'ID')
		{
			$updateVariable .= ", $key = '" . $this->DBH->real_escape_string(stripslashes($value))."'";
		}
	}	
	
	// отстраняваме първата запетая, с която започва $insertFields
	$insertFields = substr($insertFields, 1);
	// отстраняваме първата запетая, с която започва $insertValues	
	$insertValues = substr($insertValues, 1);
	$updateVariable = substr($updateVariable, 1);
	// и сега SQL заявката се формира така:
	
	if(empty($id))
	{		
		$query = "INSERT INTO Players($insertFields) VALUES($insertValues)";
		$msg = 'plr_added';
	}
	else
	{		
		$query = "UPDATE Players SET $updateVariable WHERE ID = '$id'";
		$msg = 'plr_edited';
	}
	// ако изведете заявката на екрана
	//print $query;
	// ще получите това:
	//INSERT INTO studenti(FN, sobstvenoIme, familnoIme, email)
	//VALUES(‘003210’, ‘Ivan’, ‘Ivanov’, ‘ivanov@gmail.com’)
	
	if ($this->DBH->query($query)) 
	{
		$this->browse('',$msg);
	}
	else
	{
		$this->addScreen($data,'plr_addEdit');
	}
}

// delete
// iztriva ukazanata specialnost i vryshta kym browse ekrana
function delete($id) 
{	
	if (empty($id)) 
	{
		// greshka pri izptrivaneto
		$this->browse('', 'plr_deleted');
	}
	
	$query = 'DELETE FROM Players WHERE ID='.intval($id);
	if (!$this->DBH->query($query)) 
	{
		$this->browse('', 'plr_deleted');
	} 
	else 
	{
		$this->browse('', 'plr_deleted');
	}
} // end of delete()

public function __destruct() {
	parent::__destruct();
}

	
} // end of class

?>