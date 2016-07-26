#ifndef BLUESROCK_H
#define BLUESROCK_H

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

#endif