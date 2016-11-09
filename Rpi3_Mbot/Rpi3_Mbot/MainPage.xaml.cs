// Copyright (c) Microsoft. All rights reserved.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.Devices.Gpio;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rpi3_Mbot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitSystemComponent();

            // Fill in your TeamName KitID and keys.
            IotHubLocalService LocalService = new IotHubLocalService("TeamName", "KitID", "keys");

            // Set Xbox controller
            XboxInit_timer();
            Xbox.Button_A_Pressed += new Xbox.ListenerHandler(Button_A_Click);
            Xbox.Button_B_Pressed += new Xbox.ListenerHandler(Button_B_Click);
            Xbox.Button_X_Pressed += new Xbox.ListenerHandler(Button_X_Click);
            Xbox.Button_Y_Pressed += new Xbox.ListenerHandler(Button_Y_Click);
            Xbox.Button_RB_Pressed += new Xbox.ListenerHandler(Button_RB_Click);
            Xbox.Button_RT_Pressed += new Xbox.ListenerHandler(Button_RB_Click);

            //Left Stick for car controller
            Xbox.Leftstick_Stop += new Xbox.ListenerHandler(Leftstick_Stop_Click);
            Xbox.Leftstick_Up += new Xbox.ListenerHandler(Leftstick_Up_Click);
            Xbox.Leftstick_Down += new Xbox.ListenerHandler(Leftstick_Down_Click);
            Xbox.Leftstick_Left += new Xbox.ListenerHandler(Leftstick_Left_Click);
            Xbox.Leftstick_Right += new Xbox.ListenerHandler(Leftstick_Right_Click);

            //Adding support for Dpad for controller
            Xbox.Dpad_Stop += new Xbox.ListenerHandler(Leftstick_Stop_Click);
            Xbox.Dpad_Up += new Xbox.ListenerHandler(Leftstick_Up_Click);
            Xbox.Dpad_Down += new Xbox.ListenerHandler(Leftstick_Down_Click);
            Xbox.Dpad_Left += new Xbox.ListenerHandler(Leftstick_Left_Click);
            Xbox.Dpad_Right += new Xbox.ListenerHandler(Leftstick_Right_Click);


            // Set LEDs
            InitLED();
            
        }

        /// <summary>
        /// Used to set buttonA for kick ball
        /// </summary>
        private void Button_A_Click()
        {
            Debug.WriteLine("ok!----button_A_Click !  ");
            Shoot.KickballCmd();
        }

        /// <summary>
        /// Used to set buttonB for stop car
        /// </summary>
        private void Button_B_Click()
        {
            Debug.WriteLine("ok!----button_B_Click !  ");
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Forward, 0);
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Forward, 0);
            Motor.DoMotorCmd();
        }

        /// <summary>
        /// Used to set buttonX for turn on yellow led and red led
        /// </summary>
        private void Button_X_Click()
        {
            Debug.WriteLine("ok!----button_X_Click !  ");
            pin2.Write(GpioPinValue.High);
            pin3.Write(GpioPinValue.High);
        }

        /// <summary>
        /// Used to set buttonY for turn off yellow led and red led
        /// </summary>
        private void Button_Y_Click()
        {
            Debug.WriteLine("ok!----button_Y_Click !  ");
            pin2.Write(GpioPinValue.Low);
            pin3.Write(GpioPinValue.Low);
        }

        /// <summary>
        /// Used to set buttonRB for kick ball
        /// </summary>
        private void Button_RB_Click()
        {
            Debug.WriteLine("ok!----button_RB_Click !  ");
            Shoot.KickballCmd();
        }

        /// <summary>
        /// Used to set Leftstick_Stop for stop car
        /// </summary>
        private void Leftstick_Stop_Click()
        {
            Debug.WriteLine("ok!----Leftstick_Stop_Click !  ");
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Forward, 0);
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Forward, 0);
            Motor.DoMotorCmd();
        }

        /// <summary>
        /// Used to set Leftstick_Up for car go
        /// </summary>
        private void Leftstick_Up_Click()
        {
            Debug.WriteLine("ok!----Leftstick_Up_Click !  ");
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Forward, 160);
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Forward, 160);
            Motor.DoMotorCmd();
        }

        /// <summary>
        /// Used to set Leftstick_Down for car back
        /// </summary>
        private void Leftstick_Down_Click()
        {
            Debug.WriteLine("ok!----Leftstick_Down_Click !  ");
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Backward, 160);
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Backward, 160);
            Motor.DoMotorCmd();
        }

        /// <summary>
        /// Used to set Leftstick_Left for car left
        /// </summary>
        private void Leftstick_Left_Click()
        {
            Debug.WriteLine("ok!----Leftstick_Left_Click ! ");
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Forward, 120);
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Backward, 120);
            Motor.DoMotorCmd();
        }

        /// <summary>
        /// Used to set Leftstick_Right for car right
        /// </summary>
        private void Leftstick_Right_Click()
        {
            Debug.WriteLine("ok!----Leftstick_Right_Click !  ");
            Motor.SetMotorCmd(Motor.ID.LeftMotor, Motor.Dir.Forward, 120);
            Motor.SetMotorCmd(Motor.ID.RightMotor, Motor.Dir.Backward, 120);
            Motor.DoMotorCmd();
        }

        DispatcherTimer xboxtimer;
        private void XboxInit_timer()
        {
            xboxtimer = new DispatcherTimer();
            xboxtimer.Interval = TimeSpan.FromMilliseconds(1000);
            xboxtimer.Tick += Xbox_Timer_Tick;
            xboxtimer.Start();
        }

        /// <summary>
        /// Used to check game contrlloer state 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Xbox_Timer_Tick(object sender, object e)
        {
            await Xbox.XboxInit();
           

            //Test the Xbox state if connectted ok
            if (Xbox.ControllerCountFlag == 1)
            {
                pin1.Write(GpioPinValue.High);
            }
            else
            {
                pin1.Write(GpioPinValue.Low);
            }

        }

        private GpioPin pin1;//Green led
        private GpioPin pin2;//Yellow led
        private GpioPin pin3;//Red led
        private int GPIO17 = 17;
        private int GPIO27 = 27;
        private int GPIO22 = 22;

        /// <summary>
        /// Used to init GPIO device
        /// </summary>
        private void InitLED()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin1 = null;
                pin2 = null;
                pin3 = null;
                return;
            }

            pin1 = gpio.OpenPin(GPIO17);
            pin2 = gpio.OpenPin(GPIO27);
            pin3 = gpio.OpenPin(GPIO22);

            // Show an error if the pin wasn't initialized properly
            if (pin1 == null || pin2 == null || pin3 == null)
            {
                return;
            }

            pin1.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            pin3.SetDriveMode(GpioPinDriveMode.Output);

           //Turn on led2 and led3
            pin2.Write(GpioPinValue.High);
            pin3.Write(GpioPinValue.High);

        }
    }

}