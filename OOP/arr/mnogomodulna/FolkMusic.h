#ifndef FOLKMUSIC_H
#define FOLKMUSIC_H

class FolkMusic : public Music
{
public:
	FolkMusic( char *, char *, char *);
	~FolkMusic () {delete fmKind;}
	virtual void print (void);
	virtual char *getClassName (void);
public:
	char *fmKind;
};

#endif