#include<iostream>
#include<cstring>

using namespace std;






class Science 
{
public:
	Science(void);
	Science(char *);
	Science(const Science &);
	virtual ~Science ( void ) { delete Name; }
	virtual void Show (void);
	virtual char *getClassName( void ) ;
	void link(Science *);
	void showme(Science *);
protected:
	char *Name;
	Science *next;
};








class Natural : public Science
{
public:
	Natural (void);
	Natural (char *,char *);
	Natural (const Natural &);
	~Natural (void) { delete Public; }
	virtual void Show(void);
	virtual char *getClassName(void) const;
protected:
	char *Public;	
}; 



class Chemist : public Natural
{
public:
	Chemist (void);
	Chemist (char *,char *, int);
	Chemist (const Chemist &);
	~Chemist (void) {}
	virtual void Show(void);
	virtual char *getClassName(void) const;
protected:
	int x;
};



class Physics : public Natural
{
public:
	Physics (void);
	Physics (char *,char *, int);
	Physics (const Physics &);
	~Physics (void) {}
	virtual void Show(void);
	virtual char *getClassName(void) const;
protected:
	int x;
};

class Mechanics : public Physics
{
public:
	Mechanics (void);
	Mechanics (char *,char *, int , int);
	Mechanics (const Mechanics &);
	~Mechanics (void) {}
	virtual void Show(void);
	virtual char *getClassName(void) const;
protected:
	int y;
}; 

class Thermo : public Physics
{
public:
	Thermo (void);
	Thermo (char *,char *, int , int);
	Thermo (const Thermo &);
	~Thermo (void) {}
	virtual void Show(void);
	virtual char *getClassName(void) const;
protected:
	int y;
};





class Social : public Science
{
public:
	Social(void);
	Social(char *, int);
	Social(const Social &);
	~Social ( void ) {};
	virtual void Show (void);
	virtual char *getClassName( void ) const;
protected:
	int koef;
};  


class Geography : public Social
{
public:
	Geography(void);
	Geography(char *, int, char *);
	Geography(const Geography &);
	~Geography ( void ) {delete Region;};
	virtual void Show (void);
	virtual char *getClassName( void ) const;
protected:
	char *Region;
}; 


class History : public Social
{
public:
	History(void);
	History(char *, int, char *);
	History(const History &);
	~History ( void ) {delete Period;};
	virtual void Show (void);
	virtual char *getClassName( void ) const;
protected:
	char *Period;
}; 









Science :: Science(void)
{
	Name = new char[strlen("NonName") + 1];
	strcpy(Name,"NonName");
	next=0;

}

Science :: Science(char *tempName)
{
	Name = new char[strlen(tempName) + 1];
	strcpy(Name, tempName);
}

Science :: Science(const Science &tempScience)
{
	Name = new char[strlen(tempScience.Name) + 1];
	strcpy(Name, tempScience.Name);
}


void Science::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Name : " << Name << endl;
}

char *Science::getClassName(void) 
{
	return " Sciences";
}

void Science ::link(Science *ptr)
{
ptr->next=next;
next=ptr;
}
void Science ::showme(Science *nach)
{
	for(Science *p=nach; p; p=p->next)
		p->Show();
}









Social :: Social(void)
{
	koef = 0;
}

Social :: Social(char *tempName , int tempKoef):Science(tempName)
{
	koef = tempKoef;
}

Social :: Social(const Social &tempSocial)
{
	Name = new char[strlen(tempSocial.Name) + 1];
	strcpy(Name, tempSocial.Name);
	koef = tempSocial.koef;
}


void Social::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Name : " << Name;
	cout << "\n koef : " << koef << endl;
}

char *Social::getClassName(void) const
{
	return " Social";
}










Natural :: Natural (void)
{
	Public = new char[strlen("Publichnost") + 1];
	strcpy(Public,"Publichnost");

}

Natural :: Natural (char *tempName,char *tempPublic):Science(tempName)
{
	Public = new char[strlen(tempPublic) + 1];
	strcpy(Public, tempPublic);
}

Natural :: Natural(const Natural &tempNatural)
{
	Name = new char[strlen(tempNatural.Name) + 1];
	strcpy(Name, tempNatural.Name);
	Public = new char[strlen(tempNatural.Public) + 1];
	strcpy(Public, tempNatural.Public);
}

void Natural::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Public activity:  " << Public;
	cout << "\n Name : " << Name << endl;
}

char *Natural::getClassName(void) const
{
	return "Natural";
}








Chemist :: Chemist (void)
{
	x = 0;
}

Chemist :: Chemist (char *tempName,char *tempPublic, int tempX):Natural(tempName, tempPublic)
{
	x = tempX;
}

Chemist :: Chemist(const Chemist &tempChemist)
{
	Name = new char[strlen(tempChemist.Name) + 1];
	strcpy(Name, tempChemist.Name);
	Public = new char[strlen(tempChemist.Public) + 1];
	strcpy(Public, tempChemist.Public);
	x = tempChemist.x;
}


void Chemist::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Public activity:  " << Public;
	cout << "\n Name : " << Name;
	cout << "\n Promenliva X:  " << x << endl;
}

char *Chemist::getClassName(void) const
{
	return "Chemist";
}








Physics :: Physics (void)
{
	x = 0;
}

Physics :: Physics (char *tempName,char *tempPublic, int tempX):Natural(tempName, tempPublic)
{
	x = tempX;
}

Physics :: Physics(const Physics &tempPhysics)
{
	Name = new char[strlen(tempPhysics.Name) + 1];
	strcpy(Name, tempPhysics.Name);
	Public = new char[strlen(tempPhysics.Public) + 1];
	strcpy(Public, tempPhysics.Public);
	x = tempPhysics.x;
}


void Physics::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Public activity:  " << Public;
	cout << "\n Name : " << Name;
	cout << "\n Promenliva X:  " << x << endl;
}

char *Physics::getClassName(void) const
{
	return "Physics";
}








Geography :: Geography(void)
{
	Region = new char[strlen("None Region") + 1];
	strcpy(Region,"None Region");
}

Geography :: Geography(char *tempName , int tempKoef, char *tempRegion):Social(tempName,tempKoef)
{
	Region = new char[strlen(tempRegion) + 1];
	strcpy(Region, tempRegion);
}

Geography :: Geography(const Geography &tempGeography)
{
	Name = new char[strlen(tempGeography.Name) + 1];
	strcpy(Name, tempGeography.Name);
	koef = tempGeography.koef;
	Region = new char[strlen(tempGeography.Region) + 1];
	strcpy(Name, tempGeography.Region);
}


void Geography::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Name : " << Name;
	cout << "\n koef : " << koef;
	cout << "\n Region : " << Region << endl;
}

char *Geography::getClassName(void) const
{
	return " Geography";
}










History :: History(void)
{
	Period = new char[strlen("1800 - 2007") + 1];
	strcpy(Period,"1800 - 2007");
}

History :: History(char *tempName , int tempKoef, char *tempPeriod):Social(tempName,tempKoef)
{
	Period = new char[strlen(tempPeriod) + 1];
	strcpy(Period, tempPeriod);
}

History :: History(const History &tempHistory)
{
	Name = new char[strlen(tempHistory.Name) + 1];
	strcpy(Name, tempHistory.Name);
	koef = tempHistory.koef;
	Period = new char[strlen(tempHistory.Period) + 1];
	strcpy(Name, tempHistory.Period);
}


void History::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Name : " << Name;
	cout << "\n koef : " << koef;
	cout << "\n Period : " << Period << endl;
}

char *History::getClassName(void) const
{
	return " History";
}







Mechanics :: Mechanics (void)
{
	y = 0;
}

Mechanics :: Mechanics (char *tempName,char *tempPublic, int tempX, int tempY):Physics(tempName, tempPublic, tempX)
{
	y = tempY;
}

Mechanics :: Mechanics(const Mechanics &tempMechanics)
{
	Name = new char[strlen(tempMechanics.Name) + 1];
	strcpy(Name, tempMechanics.Name);
	Public = new char[strlen(tempMechanics.Public) + 1];
	strcpy(Public, tempMechanics.Public);
	x = tempMechanics.x;
	y = tempMechanics.y;
}


void Mechanics::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Public activity:  " << Public;
	cout << "\n Name : " << Name;
	cout << "\n Promenliva X:  " << x << endl;
	cout << "Promenliva Y:  " << y << endl;
}

char *Mechanics::getClassName(void) const
{
	return "Mechanics";
}







Thermo :: Thermo (void)
{
	y = 0;
}

Thermo :: Thermo (char *tempName,char *tempPublic, int tempX, int tempY):Physics(tempName, tempPublic, tempX)
{
	y = tempY;
}

Thermo :: Thermo(const Thermo &tempThermo)
{
	Name = new char[strlen(tempThermo.Name) + 1];
	strcpy(Name, tempThermo.Name);
	Public = new char[strlen(tempThermo.Public) + 1];
	strcpy(Public, tempThermo.Public);
	x = tempThermo.x;
	y = tempThermo.y;
}


void Thermo::Show (void)
{	
	cout << "\n Class: " << getClassName();
	cout << "\n Public activity:  " << Public;
	cout << "\n Name : " << Name;
	cout << "\n Promenliva X:  " << x << endl;
	cout << "Promenliva Y:  " << y << endl;
}

char *Thermo::getClassName(void) const
{
	return "Thermodynamics";
}



void Print(Science *temp[]);

//======================  MAIN  ====================
void main (void)
{
Science *sc= new Science("TEST");
Science *nat= new Natural;
Science *ch= new Chemist;
Science *ps= new Physics;
Science *th= new Thermo;
Science *me= new Mechanics;
Science *so= new Social;
Science *geo= new Geography;
Science *hi= new History;
Science *point= sc;
sc->link(nat);
	nat->link(ch);
	ch->link(ps);
	ps->link(th);
	th->link(me);
	me->link(so);
	so->link(geo);
	geo->link(hi);
	point->showme(point);
}
