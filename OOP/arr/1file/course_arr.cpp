#include<iostream>
#include<cstring>
#include<iomanip>

using namespace std;


class Music
{
public:
	Music ( char *,char *);
	virtual ~Music() {delete song; }
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *song;
	char *author;
	
};




class Classic : public Music
{
public:
	Classic( char *,char *,char *);
	~Classic() {delete cKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *cKind;
};




class Pop : public Music
{
public:
	Pop ( char *, char *,char *);
	~Pop () {delete pKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *pKind;
};




class FolkMusic : public Music
{
public:
	FolkMusic( char *, char *, char *);
	~FolkMusic () {delete fmKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *fmKind;
};




class Rock : public Music
{
public:
	Rock ( char *, char *,char *);
	~Rock () {delete rKind;}
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *rKind;
};




class Metal : public Rock
{
public:
	Metal ( char* , char * , char * ,char *);
	~Metal ()  {delete meKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *meKind;
};




class BluesRock : public Rock
{
public:
	BluesRock ( char *, char * , char *, char *);
	~BluesRock () {delete bKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *bKind;
};


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
	
}

void printArr (Music *a[])
{
	for(int i=0;i<7;i++)
	{
		cout<<"Class Name : "<<a[i]->getClassName()<<endl;
		a[i]->print();
	}
}








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




Classic :: Classic (char *inSong ,char *inAuthor,char *c):Music(inSong,inAuthor)
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


Pop :: Pop (char *inSong ,char *inAuthor,char *p):Music(inSong,inAuthor)
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
