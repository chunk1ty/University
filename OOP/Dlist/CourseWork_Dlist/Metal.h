#ifndef METAL_H
#define METAL_H

class Metal : public Rock
{
public:
	Metal ( char *inSong="Come On In My Kitchen",char*inAuthor="Robert Jphnson" ,char *r="rock" ,char *me="metal");
	~Metal ()  {delete meKind;}
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *meKind;
};

#endif