#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"
#include "Rock.h"
#include "Metal.h"

Metal :: Metal (char *inSong,char *inAuthor, char *r,char *me):Rock(inSong ,inAuthor,r)
{
	meKind=new char [strlen(me)+1];
	strcpy (meKind,me);
}

void Metal :: print (void)
{
	Rock::print();
	cout<<setw(17)<<"Subgenre  : "<<meKind<<endl;
	
}

char *Metal::getClassName(void)
{
	return "Metal";
}