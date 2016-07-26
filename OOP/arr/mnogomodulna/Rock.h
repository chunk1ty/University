#ifndef ROCK_H
#define ROCK_H

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

#endif