#include<iostream>
#include<iomanip>
#include<cstring>

using namespace std;

#include "Music.h"
#include "Rock.h"
#include "BluesRock.h"

BluesRock :: BluesRock (char *inSong,char *inAuthor, char *r,char *b):Rock(inSong ,inAuthor,r)
{
	bKind=new char [strlen(b)+1];
	strcpy (bKind,b);
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

