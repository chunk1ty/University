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
#include "Year.h"

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
	Music *y=new Year;

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

	q=q1;
	q1=y;
	q->link(q1);

	list->printlist(list);

	delete m;
	delete c;
	delete p;
	delete fm;
	delete r;
	delete b;
	delete me;
	delete y;

}