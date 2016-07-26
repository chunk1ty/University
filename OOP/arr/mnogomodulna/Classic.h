#ifndef CLASSIC_H
#define CLASSIC_H

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

#endif