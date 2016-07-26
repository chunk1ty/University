#include<iostream>
#include<cstring>

using namespace std;

class Animal
{
public:
	Animal( char *a = " Jivotno " );
	virtual ~Animal( )
		{ delete kind; }
	virtual void print( void );
	void link( Animal * );
	void printlist( Animal * );
protected:
	char *kind;
	Animal *next;
};

class Boza : public Animal
{
public:
	Boza( char *b = " Bozainik " );
	~Boza( )
		{ delete rod; }
	virtual void print( void );
protected:
	char *rod;
};

class Hisht : public Boza
{
public:
	Hisht( char *h = " Xi6tnik " );
	~Hisht( )
		{ delete vid; }
	virtual void print( void );
protected:
	char *vid;
};

class Tigar : public Hisht
{
public:
	Tigar( char *t = " Tigar " );
	~Tigar( )
		{ delete name; }
	virtual void print( void );
private:
	char *name;
};

void main (void)

{
	Animal *p,*p1;
	Animal *a=new Animal;
	Animal *b=new Boza;
	Animal *h=new Hisht;
	Animal *t=new Tigar;
	p=a;
	Animal *list=p;
	p1=h;
	p->link(p1);

	p=p1;
	p1=b;
	p->link(p1);

	p=p1;
	p1=t;
	p->link(p1);

	list->printlist(list);

	delete a;
	delete b;
	delete h;
	delete t;
	
}


Animal :: Animal( char *a )
{
	next=0;
	kind = new char[strlen(a)+1];
	strcpy( kind, a );
}

void Animal :: print(void)
{
	cout<<"Tozi obekt e ot klasa Animal"<<kind<<endl;
}

void Animal :: link (Animal *pt)
{
	pt->next=next;
	next=pt;
}

void Animal ::printlist (Animal *beg)
{
	for(Animal *pt=beg;pt;pt=pt->next)
		pt->print();
}


Boza :: Boza( char *b )
{
	rod = new char[strlen(b)+1];
	strcpy( rod, b );
}

void Boza :: print(void)
{
	Animal :: print ();
	cout<<"Tozi obekt e ot klasa Bozainik"<<rod<<endl;

}


Hisht :: Hisht( char *h )
{
	vid = new char[strlen(h)+1];
	strcpy( vid, h );
}


void Hisht :: print( void )
{
	Boza :: print();
	cout << " Tozi obekt e ot klasa Hi6tnik " << vid << "\n";

}


Tigar :: Tigar( char *t )
{
	name = new char[strlen(t)+1];
	strcpy( name, t );
}


void Tigar :: print( void )
{
	Hisht :: print();
	cout << " Tozi obekt e ot klasa Tigar " << name << "\n";

}
