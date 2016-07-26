#ifndef YEAR_H
#define YEAR_H

class Year  : public Metal
{
public:
	Year ( char *inSong="Hate Me!",char *inAuthor = "Children of Bodom",char *r="rock",char *me="metal",int y=2000); 
	 ~Year (void) {};
	virtual void print (void);
	virtual char *getClassName (void);
private:
	int god;
};

#endif



