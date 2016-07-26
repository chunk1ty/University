#ifndef POP_H
#define POP_H

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

#endif