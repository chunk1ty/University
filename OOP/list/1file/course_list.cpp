#include<iostream>
#include<cstring>

using namespace std;


class Music
{
public:
	Music ( char *inSong = "Pass Out", char *inAuthor="Tinie Tempah");
	virtual ~Music() {delete song; delete author;}
	virtual void print (void);
	virtual char *getClassName (void);
	void link(Music * );
	void printlist(Music *);
protected:
	char *song;
	char *author;
	Music *next;
};




class Classic : public Music
{
public:
	Classic( char *inSong="MoonLight Sonata" ,char *inAuthor="Ludwing v.Beethoven" , char * c="classic");
	~Classic() {delete cKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *cKind;
};




class Pop : public Music
{
public:
	Pop ( char *inSong="Hips Dont Lie",char *inAuthor="Shakira" , char *p="pop");
	~Pop () {delete pKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *pKind;
};




class FolkMusic : public Music
{
public:
	FolkMusic( char *inSong="Eleno Mome",char *inAuthor="Hristina Boteva",char *fm="folk");
	~FolkMusic () {delete fmKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *fmKind;
};




class Rock : public Music
{
public:
	Rock ( char *inSong="Smelles Like Tenn Spirit",char*inAuthor="Nirvana",char*r="rock");
	~Rock () {delete rKind;}
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *rKind;
};









class BluesRock : public Rock
{
public:
	BluesRock ( char *inSong="Stand Alone",char*inAuthor="Godsmack" ,char *r="rock" ,char *me="rockandroll");
	~BluesRock () {delete bKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *bKind;
};

class Metal : public Rock
{
public:
	Metal ( char *inSong="Come On In My Kitchen",char*inAuthor="Robert Jphnson" ,char *r="rock" ,char *me="metal");
	~Metal ()  {delete meKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *meKind;
};




void main (void)

{
	Music *q,*q1;

	Music *m=new Music;
	Music *c=new Classic;
	Music *p=new Pop;
	Music *fm=new FolkMusic;
	Music *r=new Rock;
	Music *b=new BluesRock;
	Music *me=new Metal;

	q=m;
	Music *list=q;
	q1=c;
	q->link(q1);

	q=q1;
	q=p;
	q->link(q1);

	q=q1;
	q1=fm;
	q->link(q1);

	q=q1;
	q1=r;
	q->link(q1);

	q=q1;
	q1=b;
	q->link(q1);

	q=q1;
	q1=me;
	q->link(q1);

	list->printlist(list);

	delete m;
	delete c;
	delete p;
	delete fm;
	delete r;
	delete b;
	delete me;

}


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
	cout<<"Class Name : "<<getClassName()<<endl;
	cout<<"Song : "<<song<<endl;
	cout<<"Performer : "<<author<<endl;
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




Classic :: Classic (char *inSong,char *inAuthor,char *c):Music (inSong , inAuthor)
{
	cKind=new char [strlen(c)+1];
	strcpy (cKind,c);
}

void Classic :: print (void)
{
	Music::print();
	cout<<"Genre"<<cKind<<endl;
}

char *Classic::getClassName(void)
{
	return "Classic";
}




Pop :: Pop (char *inSong,char *inAuthor,char *p):Music (inSong , inAuthor)
{
	pKind=new char [strlen(p)+1];
	strcpy (pKind,p);
}

void Pop :: print (void)
{
	Music::print();
	cout<<"Genre"<<pKind<<endl;
}

char *Pop::getClassName(void)
{
	return "Pop";
}



FolkMusic :: FolkMusic (char *inSong,char *inAuthor,char *fm):Music (inSong , inAuthor)
{
	fmKind=new char [strlen(fm)+1];
	strcpy (fmKind,fm);
}

void FolkMusic :: print (void)
{
	Music::print();
	cout<<"Genre"<<fmKind<<endl;
}

char *FolkMusic::getClassName(void)
{
	return "FolkMusic";
}






Rock :: Rock (char *inSong,char *inAuthor,char *r):Music (inSong , inAuthor)
{
	rKind=new char [strlen(r)+1];
	strcpy (rKind,r);
}

void Rock :: print (void)
{
	Music::print();
	cout<<"Genre"<<rKind<<endl;
}


char *Rock::getClassName(void)
{
	return "Rock";
}



BluesRock :: BluesRock (char *inSong,char *inAuthor,char *r,char *rNr):Rock (inSong,inAuthor)
{
	bKind=new char [strlen(rNr)+1];
	strcpy (bKind,rNr);
}

void BluesRock :: print (void)
{
	Rock::print();
	cout<<"Subgenre"<<bKind<<endl;
}

char *BluesRock::getClassName(void)
{
	return "BluesRock";
}





Metal :: Metal (char *inSong,char *inAuthor,char *r,char *me):Rock (inSong,inAuthor,r)
{
	meKind=new char [strlen(me)+1];
	strcpy (meKind,me);
}

void Metal :: print (void)
{
	Rock::print();
	cout<<"Subgenre"<<meKind<<endl;
}



char *Metal::getClassName(void)
{
	return "Metal";
}