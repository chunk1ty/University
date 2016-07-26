#include<iostream>
#include<iomanip>
#include<cstring>

using namespace std;

#include "Music.h"
#include "Rock.h"
#include "BluesRock.h"

BluesRock :: BluesRock (char *inSong,char *inAuthor,char *r,char *rNr):Rock (inSong,inAuthor)
{
	bKind=new char [strlen(rNr)+1];
	strcpy (bKind,rNr);
}

void BluesRock :: print (void)
{
	
	Rock::print();
	cout<<setw(17)<<"Subgenre  : "<<bKind<<endl;
	
}

char *BluesRock::getClassName(void)
{
	return "BluesRock";
}