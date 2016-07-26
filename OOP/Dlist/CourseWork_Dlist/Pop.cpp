#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"
#include "Pop.h"

Pop :: Pop (char *inSong,char *inAuthor,char *p):Music (inSong , inAuthor)
{
	pKind=new char [strlen(p)+1];
	strcpy (pKind,p);
}

void Pop :: print (void)
{
	
	Music::print();
	cout<<setw(17)<<"Genre     : "<<pKind<<endl;
	
}

char *Pop::getClassName(void)
{
	return "Pop";
}

