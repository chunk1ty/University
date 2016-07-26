import java.awt.Color;
import java.awt.Graphics;

public class UnfilledRectangle extends Figures implements Shape
{	
	public UnfilledRectangle(int x1, int y1, int x2, int y2, Color colorInitial) 
	{
		super(x1, y1, x2, y2, colorInitial);		
	}

	public void drawShape(Graphics g) 
	{		
		int width = super.x1 - super.x2;
    	int height = super.y1 - super.y2;

    	super.w = Math.abs( width );
    	super.h = Math.abs( height );
    	super.x = width < 0 ? super.x1 : super.x2;
    	super.y = height < 0 ? super.y1 : super.y2; 
    	g.setColor(super.color);
    	g.drawRect( super.x, super.y, super.w, super.h );    	    	    	 
	}
	
	public String toString()
	{		
		String colorAsString = null;
		StringBuffer sb = new StringBuffer();
		
		sb.append("Unfilled Rectangle : ");		
		sb.append("x = " + super.x);
		sb.append(" y = " + super.y);
		sb.append(" width = " + super.w);
		sb.append(" height = " + super.h);
		sb.append(" Color =" + super.ColorAsString(colorAsString));
		
		return sb.toString();
	}
}
