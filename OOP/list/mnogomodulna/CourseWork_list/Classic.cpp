#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"
#include "Classic.h"

Classic :: Classic (char *inSong,char *inAuthor,char *c):Music (inSong , inAuthor)
{
	cKind=new char [strlen(c)+1];
	strcpy (cKind,c);
}

void Classic :: print (void)
{
	
	Music::print();
	cout<<setw(17)<<"Genre     : "<<cKind<<endl;
	
}

char *Classic::getClassName(void)
{
	return "Classic";
}
