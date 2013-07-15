#include <p18f2525.h>
#include <adc.h>
#include <delays.h>
#include <usart.h>
#include <i2c.h>
#include <stdio.h>


//********** Configuration bits **********//
#pragma config OSC = INTIO7 //Internal oscillator, output to pin 10
#pragma config FCMEN = OFF //Fail-safe clock monitor disabled
#pragma config IESO = OFF //Int.-ext. oscillator switch over disabled
#pragma config PWRT = OFF //Power-up timer disabled
#pragma config BOREN = OFF //Brown-out reset disabled
#pragma config WDT = OFF //Watch-dog timer disabled
#pragma config MCLRE = ON //Master clear enabled
#pragma config LPT1OSC = OFF //T1 oscillator disabled
#pragma config PBADEN = OFF //Port B analogue-digital disabled on reset
#pragma config STVREN = OFF //Stack overflow reset disabled
#pragma config LVP = OFF //Low voltage programming disabled
#pragma config XINST = OFF //XINST disabled
#pragma config DEBUG = OFF //Background debugger disabled
#pragma config CP0 = OFF //Code protection disabled
#pragma config CP1 = OFF
#pragma config CP2 = OFF
#pragma config CPB = OFF //Boot block code protection disabled
#pragma config CPD = OFF //Data EEPROM code protection disabled
#pragma code // shows that the code starts

/* MACRO to turn on and off PIN 3  */
#define LedOn() PORTAbits.RA1=1
#define LedOff() PORTAbits.RA1=0

//init device function prototype
void initpic();
//function protoype to format number
void numberToAscii(int number);
//function prototype to print max and min value
void PrintMINMAX();
//function prototype to convert  hex to ascii
char IntToAscii(int a);

//MIN and MAX number variable initialization
unsigned int MIN = 0xFFFF;
unsigned int MAX = 0;

void main()
{
	//ADC result initialized to zero
	int result=0;
	
	//initialise device
	initpic();
	//initialise serial port to baud rate 115200
	OpenUSART(USART_TX_INT_OFF&USART_RX_INT_OFF&USART_ASYNCH_MODE&USART_EIGHT_BIT&USART_BRGH_HIGH,16);
	
	

	//application loop
while(1)
{

		//check for timer overflow
		if(INTCONbits.TMR0IF)
		{
 			//reloading timer0 registers
			TMR0H = 0xC1;
			TMR0L = 0x80;
			
			//Turn LED on
			LedOn();
			//start conversion
			ConvertADC(); 
			//wait till conversion is done
			while( BusyADC() ); 
			//read values
 	 		result = ReadADC();
	
			MAX = result > MAX ? result:MAX; //evaluate max value
		
			MIN = result< MIN ? result:MIN;	//evaluate min value
		
		
		
  			numberToAscii(result);     //print adc value
	
			INTCONbits.TMR0IF = 0;    //set overflow flag back to zero
		
			LedOff();    //Turn LED off   

		}//end of timer overflow if


		if(PORTAbits.RA2==0) // if button pressed
		{
	
 			PrintMINMAX(); //print MAX and MIN value
	
	 		while(PORTAbits.RA2==0);
			//RESET MAX and MIN
 			MIN = 0xFFFF; 
 			MAX = 0;

		}//end of button pres if

}//end of while loop

}//end of main function


// function to initialise the PIC
void initpic(){

	
	ADCON0 = 0x01; //enable adc  on channel zero
	ADCON1 = 0x0E; //make RA0 and other RA pin Digital  I/O
	ADCON2 = 0xA6; //right justification

	TRISA=0xFF;    
	TRISAbits.TRISA1 = 0;
	OSCCON = 0x72; //8 MHz Int oscillator with Fosc/4 output
	BAUDCONbits.BRG16 = 1; // Use 16-bit Baud rate


	T0CONbits.T08BIT = 0; //timer 0 in 16 bit mode
	T0CONbits.T0CS = 0; // internal oscillator source
	TMR0H = 0xC1; //preload timer 0
	TMR0L = 0x80; 
	INTCONbits.TMR0IF = 0; //timer 0 reset interrupt flag
	T0CONbits.TMR0ON = 1; // start tmier 0 running
	INTCONbits.TMR0IE = 0; //disable interrupt on timer 0
	INTCONbits.GIE = 0; // disable global interrupt
	LedOff();

}//end of initpic function

// function to print MAX and MIN value
void PrintMINMAX()
{
	//initialise temporary variable to hold lower order bits
	int AMIN=0,BMIN=0,CMIN=0,DMIN=0;
	int ATMIN=0,BTMIN=0,CTMIN=0,DTMIN=0;
	int AMAX=0,BMAX=0,CMAX=0,DMAX=0;
	int ATMAX=0,BTMAX=0,CTMAX=0,DTMAX=0;

	//string to hold min and max number string in ascii
	char MessageMIN[7];	
	char MessageMAX[7];	

	//Bitwise operations for MIN and MAX numbers
	ATMAX= MAX << 12;
	AMAX =ATMAX>>12;

	BTMAX= MAX << 8;
	BMAX =BTMAX>>12;

	CTMAX= MAX << 4;
	CMAX =CTMAX>>12;

	DMAX = MAX>>12;


	ATMIN= MIN << 12;
	AMIN =ATMIN>>12;

	BTMIN= MIN << 8;
	BMIN =BTMIN>>12;

	CTMIN= MIN << 4;
	CMIN =CTMIN>>12;

	DMIN = MIN>>12;

	//0x for Maximum number string
	MessageMAX[0] = 0x30;
	MessageMAX[1] = 0x78;
	//Fill  the Array with Ascii values
	MessageMAX[2] = IntToAscii(DMAX);
	MessageMAX[3] = IntToAscii(CMAX);
	MessageMAX[4] = IntToAscii(BMAX);
	MessageMAX[5] = IntToAscii(AMAX);
	MessageMAX[6] = 0;

	//0x for Minimum number string
	MessageMIN[0] = 0x30;
	MessageMIN[1] = 0x78;

	MessageMIN[2] = IntToAscii(DMIN);
	MessageMIN[3] = IntToAscii(CMIN);
	MessageMIN[4] = IntToAscii(BMIN);
	MessageMIN[5] = IntToAscii(AMIN);
	MessageMIN[6] = 0;
  
	//print format string to serial port
	printf("MAX=%s\r\nMIN=%s\r\n#\r\n",MessageMAX,MessageMIN);
	

}

//function to format number
void numberToAscii(int number)
{
	//initialise temporary variable to hold lower order bits
	int A=0,B=0,C=0,D=0;
	int AT=0,BT=0,CT=0,DT=0;
	//MEssage string
	char Message[7];	
	AT= number << 12;
	A =AT>>12;

	BT= number << 8;
	B =BT>>12;

	CT= number << 4;
	C =CT>>12;

	D = number>>12;


	Message[0] = 0x30;
	Message[1] = 0x78;
	Message[2] = IntToAscii(D);
	Message[3] = IntToAscii(C);
	Message[4] = IntToAscii(B);
	Message[5] = IntToAscii(A);
	Message[6] = 0;
  
	putsUSART(Message);
	putrsUSART("\r\n");
	
}

//Convert integer to its ascii representation
char IntToAscii(int a)
{

	switch(a)
	{
	case 0:
	return 0x30;
	case 1:
	return 0x31;
	case 2:
	return 0x32;
	case 3:
	return 0x33;
	case 4:
	return 0x34;
	case 5:
	return 0x35;
	case 6:
	return 0x36;
	case 7:
	return 0x37;
	case 8:
	return 0x38;
	case 9:
	return 0x39;

	case 10:
	//A
	return 0x41;

	case 11:
	//B
	return 0x42;

	case 12:
	//C
	return 0x43;

	case 13:
	//D
	return 0x44;
	case 14:
	//E
	return 0x45;
	case 15:
	//F
	return 0x46;
	default:
	return 0x30;

	}

}



