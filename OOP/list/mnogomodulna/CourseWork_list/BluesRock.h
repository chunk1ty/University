#ifndef BLUESROCK_H
#define BLUESROCK_H

class BluesRock : public Rock
{
public:
	BluesRock ( char *inSong="Stand Alone",char*inAuthor="Godsmack" ,char *r="rock" ,char *me="bluesrock");
	~BluesRock () {delete bKind;}
	virtual void print (void);
	virtual char *getClassName (void);
private:
	char *bKind;
};

#endif
