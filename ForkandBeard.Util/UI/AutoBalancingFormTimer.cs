using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ForkandBeard.Util.UI
{
    public partial class AutoBalancingFormTimer : System.Windows.Forms.Timer
    {
        private bool _TockLongerThanMaxIntervalOccured;
        private int _MinInterval = -1;
        private int _MaxInterval = -1;
        public event BalancedTockEventHandler BalancedTock;
        public delegate void BalancedTockEventHandler();
        public event TockLongerThanMaxIntervalEventHandler TockLongerThanMaxInterval;
        public delegate void TockLongerThanMaxIntervalEventHandler(double pdblDuration);
        public event AverageTockIntervalResumedEventHandler AverageTockIntervalResumed;
        public delegate void AverageTockIntervalResumedEventHandler();

        public int MinInterval
        {
            get { return this._MinInterval; }
            set { this._MinInterval = value; }
        }

        public int MaxInterval
        {
            get { return this._MaxInterval; }
            set { this._MaxInterval = value; }
        }

        private void AutoBalancingFormTimer_Tick(object sender, System.EventArgs e)
        {
            System.DateTime dteStart = default(System.DateTime);
            double dblDuration = 0;
            dteStart = System.DateTime.Now;

            if (BalancedTock != null)
            {
                BalancedTock();
            }
            dblDuration = System.DateTime.Now.Subtract(dteStart).TotalMilliseconds;


            if (this.MinInterval != -1 && this.MaxInterval != -1)
            {
                if (dblDuration > this.MaxInterval)
                {
                    if (!this._TockLongerThanMaxIntervalOccured)
                    {
                        this._TockLongerThanMaxIntervalOccured = true;
                        if (TockLongerThanMaxInterval != null)
                        {
                            TockLongerThanMaxInterval(dblDuration);
                        }
                    }
                }

                if ((dblDuration * 1.1) > this.Interval)
                {
                    this.Interval = Convert.ToInt32(Math.Min(dblDuration * 1.2, this.MaxInterval));
                }
                else if (this.Interval != this.MinInterval)
                {
                    if (dblDuration * 1.1 <= this.Interval)
                    {
                        this.Interval = Convert.ToInt32(Math.Max(this.Interval * 0.8, this.MinInterval));

                        if (this._TockLongerThanMaxIntervalOccured && this.Interval <= (this.MaxInterval - this.MinInterval))
                        {
                            this._TockLongerThanMaxIntervalOccured = false;
                            if (AverageTockIntervalResumed != null)
                            {
                                AverageTockIntervalResumed();
                            }
                        }
                    }
                }
            }
        }

        public AutoBalancingFormTimer(System.ComponentModel.IContainer pobjContainer)
            : base(pobjContainer)
        {
            Tick += AutoBalancingFormTimer_Tick;
        }
    }
}