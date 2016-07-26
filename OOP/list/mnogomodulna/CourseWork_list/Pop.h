#ifndef POP_H
#define POP_H

class Pop : public Music
{
public:
	Pop ( char *inSong="Hips Dont Lie",char *inAuthor="Shakira" , char *p="pop");
	~Pop (void) {delete pKind;}
	virtual void print (void);
	virtual char *getClassName (void);
private:
	char *pKind;
};

#endif