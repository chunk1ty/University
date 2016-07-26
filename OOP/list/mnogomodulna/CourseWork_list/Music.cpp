#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"

Music :: Music (char *inSong , char *inAuthor)
{
	song = new char [strlen(inSong)+1];
	strcpy(song,inSong);
	author = new char [strlen(inAuthor)+1];
	strcpy(author,inAuthor);
	next=0;
}

void Music :: print (void)
{
	cout<<endl;
	cout<<"Class Name : "<<getClassName()<<endl;
	cout<<setw(17)<<"Song      : "<<song<<endl;
	cout<<setw(17)<<"Performer : "<<author<<endl;
}

char *Music::getClassName(void)
{
	return "Music";
}

void Music :: link (Music *ptr)
{
	ptr->next=next;
	next=ptr;
}

void Music :: printlist (Music *beg)
{
	for(Music *np=beg;np;np=np->next)
			np->print();
}