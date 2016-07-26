import java.awt.*;
import java.awt.event.*;
import java.util.Iterator;
import java.util.Map.Entry;

public class WindowsPaint extends Frame implements MouseMotionListener, MouseListener
{
	private static final long serialVersionUID = 1L;	
	private int x1 = 0;
	private int x2 = 0;
	private int y1 = 0;
	private int y2 = 0;
	private MainWindows main = new MainWindows();	
	private int key = 0;
	
	public WindowsPaint ()
	{	
		this.setTitle("Work Space");
		this.setResizable(false);
		this.setLocation(205,0);
		this.setSize(600, 600);  
		this.setVisible(true);
		this.setBackground(Color.white);
		
		this.addWindowListener(new WindowAdapter()
		{		
			public void windowClosing(WindowEvent e) 
			{
				System.exit(0);
			}
		});		
    	
		this.addMouseMotionListener(this);
		this.addMouseListener(this);		
	}	
	
	 public void paint(Graphics g)
	 {		 
		 Color color = main.fillColor;		 
		 Iterator<Entry<Integer, Figures>> it = main.hashMap.entrySet().iterator();
		 
		 while(it.hasNext())
		 {
			 int key = it.next().getKey();
			 main.hashMap.get(key).drawShape(g);
		 }
		 
		 if	(main.currentFigure == 1)
		 {	
			 Line line = new Line(x1,y1,x2,y2,color);				
			 line.drawShape(g);		 
		 }
		 else if (main.currentFigure == 2)
		 {	
			 UnfilledRectangle unRec = new UnfilledRectangle(x1,y1,x2,y2,color);				
			 unRec.drawShape(g);			 
		 }
		 else if (main.currentFigure == 3)
		 {	
			 FilledRectangle rec = new FilledRectangle(x1,y1,x2,y2,color);
			 rec.drawShape(g);			 
		 }		
	 }	
	 
	 @Override
	public void mousePressed(MouseEvent event)
	{		 
		this.x1 = event.getX();
		this.y1 = event.getY();		
	}

	@Override
	public void mouseReleased(MouseEvent event) 
	{	
		Color color = main.fillColor;
		
		this.x2 = event.getX();
		this.y2 = event.getY();		
		 
		 if	(main.currentFigure == 1)
		 {		
			 Line line = new Line(x1,y1,x2,y2,color);
			 main.hashMap.put(key, line);
			 key++;
		 }
		 else if (main.currentFigure == 2)
		 {
			 UnfilledRectangle unRec= new UnfilledRectangle(x1,y1,x2,y2,color);
			 main.hashMap.put(key, unRec);
			 key++;
		 }
		 else if (main.currentFigure == 3)
		 {
			 FilledRectangle rec =new FilledRectangle(x1,y1,x2,y2,color);
			 main.hashMap.put(key, rec);
			 key++;
		 }
		 
		 repaint();
	}

		@Override
	public void mouseDragged(MouseEvent event)
	{			
		this.x2 = event.getX();
		this.y2 = event.getY();
		
		repaint();			
	}
	 
	@Override
	public void mouseClicked(MouseEvent arg0)
	{		
	}

	@Override
	public void mouseEntered(MouseEvent arg0) 
	{				
	}

	@Override
	public void mouseExited(MouseEvent arg0) 
	{				
	}	

	@Override
	public void mouseMoved(MouseEvent arg0)
	{		
	}		
 }