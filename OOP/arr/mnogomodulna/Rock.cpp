#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"
#include "Rock.h"

Rock :: Rock (char *inSong ,char *inAuthor,char *r):Music(inSong,inAuthor)
{
	rKind=new char [strlen(r)+1];
	strcpy (rKind,r);
}

void Rock :: print (void)
{
	Music::print();
	cout<<setw(17)<<"Genre     : "<<rKind<<endl;
	
}

char *Rock::getClassName(void)
{
	return "Rock";
}