#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;

#include "Music.h"

Music :: Music (char *inSong , char *inAuthor  )
{
	song = new char [strlen(inSong)+1];
	strcpy(song,inSong);
	author = new char [strlen(inAuthor)+1];
	strcpy(author,inAuthor);
	
}

void Music :: print (void)
{
	
	cout<<setw(17)<<"Song      : "<<song<<endl;
	cout<<setw(17)<<"Performer : "<<author<<endl;
}

char *Music::getClassName(void)
{
	return "Music";
}