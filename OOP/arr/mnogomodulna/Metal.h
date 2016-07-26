#ifndef METAL_H
#define METAL_H

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

#endif