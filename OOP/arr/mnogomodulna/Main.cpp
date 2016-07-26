#include<iostream>
#include<cstring>

using namespace std;

#include "Music.h"
#include "Classic.h"
#include "Pop.h"
#include "FolkMusic.h"
#include "Rock.h"
#include "BluesRock.h"
#include "Metal.h"

void printArr (Music *a[]);


void main (void)
{
	Music *family[7];
	Music *m = new Music ("Pass out","Tinie Tempah" );
	Music *c = new Classic ("Moonlight Sonata","Ludwing v. Beethoven","Classic" );
	Music *p = new Pop ("Hips Dont Lie","Shakira","Pop" );
	Music *fm = new FolkMusic ("Eleno Mome","Hristina Boteva ","Folk" );
	Music *r = new Rock ("Smells Like Teen Spirit ","Nirvana","Rock");
	Music *b = new BluesRock ("Come On In My Kitchen ","Robert Johnson","Rock","Bluesrock" );
	Music *me = new Metal ("Stand Alon ","Godsmack","Rock","Metal" );

	family[0] = m;
	family[1] = c;
	family[2] = p;
	family[3] = fm;
	family[4] = r;
	family[5] = b;
	family[6] = me;

    printArr(family);

	delete m;
	delete c;
	delete p;
	delete fm;
	delete r;
	delete b;
	delete me;
	
}

void printArr (Music *a[])
{
	for(int i=0;i<7;i++)
	{
		cout<<"Class Name : "<<a[i]->getClassName()<<endl;
		a[i]->print();
		cout<<endl;
	}
}