	 
	 
LDA	 &0
BRZ	ET 7
	 
	 
LDA	 &1
BRZ	ET 7
	 
LDA	 5
ADD	 6
STA	 &2
	 
LDA	 &2
STA	 a
	 
JMP	ET 2
	 
BRK		
