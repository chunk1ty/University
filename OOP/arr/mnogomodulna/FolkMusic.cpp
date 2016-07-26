#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"
#include "FolkMusic.h"

FolkMusic :: FolkMusic (char *inSong ,char *inAuthor,char *fm):Music(inSong,inAuthor)
{
	fmKind=new char [strlen(fm)+1];
	strcpy (fmKind,fm);
}

void FolkMusic :: print (void)
{
	Music::print();
	cout<<setw(17)<<"Genre     : "<<fmKind<<endl;
	
}


char *FolkMusic::getClassName(void)
{
	return "FolkMusic";
}