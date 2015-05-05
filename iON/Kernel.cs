using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace iON
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Welcome to iON Operating System.\nTo see the list of available commands, type 'list'.\n\n");
        }

        protected override void Run()
        {
            string ch, temp;
            int flag;
            while (true)
            {
                flag = 0;
                Console.Write(":~$ ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "shutdown":
                        {
                            CosmosOS.ACPI.Shutdown();
                            CosmosOS.ACPI.Disable();
                            Cosmos.Core.Global.CPU.Halt(); 
                            break;
                        }
                    case "list":
                        {
                            Console.WriteLine("schedule - Demonstrate CPU Scheduling algorithms");
                            Console.WriteLine("about - Info. about the OS");
                            Console.WriteLine("list - List the available commands");
                            Console.WriteLine("shutdown - Shut down the OS");
                            Console.WriteLine("reboot - Reboot the OS");
                            break;
                        }
                    case "about":
                        {
                            Console.WriteLine("iON OS v1.0 - A simple OS to demonstrate CPU Scheduling Algorithms.");
                            break;
                        }
                    case "reboot":
                        {
                            CosmosOS.ACPI.Reboot();
                            break;
                        }
                    case "schedule":
                        {
                            Int32 n, i, j, total=0, tempvar, pos, tq, count, sq=0, swt=0, stat=0;
                            float avwt = 0, avtat = 0;
                            Int32[] bt = new Int32[20];
                            Int32[] p = new Int32[20];
                            Int32[] wt = new Int32[20];
                            Int32[] st = new Int32[20];
                            Int32[] tat = new Int32[20];
                            Int32[] pr = new Int32[20];
                            while (flag == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("\nPROCESS SCHEDULING\n\nChoose from the following options : ");
                                Console.WriteLine("1. First Come First Serve (FCFS)");
                                Console.WriteLine("2. Shortest Job First (SJF)");
                                Console.WriteLine("3. Priority Based");
                                Console.WriteLine("4. Round Robin");
                                Console.WriteLine("5. Exit");
                                Console.WriteLine("\n\n\nEnter your choice : ");
                                ch = Console.ReadLine();
                                switch (ch)
                                {
                                    case "1":
                                        {
                                            Console.Write("Enter total number of processes : ");
                                            temp = Console.ReadLine();
                                            n = Int32.Parse(temp);
                                            Console.Write("\nEnter Process Burst Time\n");
                                            for (i = 0; i < n; i++)
                                            {
                                                Console.Write("P[" + (i + 1) + "]:");
                                                temp = Console.ReadLine();
                                                bt[i] = Int32.Parse(temp);
                                            }
                                            wt[0] = 0;    //waiting time for first process is 0
                                            //calculating waiting time
                                            for (i = 1; i < n; i++)
                                            {
                                                wt[i] = 0;
                                                for (j = 0; j < i; j++)
                                                    wt[i] += bt[j];
                                            }
                                            Console.WriteLine("\nProcess\t\tBT\tWT\tTT");
                                            //calculating turnaround time
                                            for (i = 0; i < n; i++)
                                            {
                                                tat[i] = bt[i] + wt[i];
                                                avwt += wt[i];
                                                avtat += tat[i];
                                                Console.WriteLine("\nP[" + (i + 1) + "]" + "\t\t" + bt[i] + "\t\t" + wt[i] + "\t\t" + tat[i]);
                                            }
                                            avwt /= i;
                                            avtat /= i;
                                            Console.WriteLine("\n\nAverage Waiting Time:" + avwt);
                                            Console.WriteLine("\nAverage Turnaround Time:" + avtat);
                                            break;
                                        }
                                    case "2":
                                        {
                                            Console.WriteLine("Enter the total number of processes: ");
                                            temp = Console.ReadLine();
                                            n = Int32.Parse(temp);               
                                            Console.Write("\nEnter Process Burst Time\n");
                                            for (i = 0; i < n; i++)
                                            {
                                                Console.Write("P[" + (i + 1) + "]:");
                                                temp = Console.ReadLine();
                                                bt[i] = Int32.Parse(temp);
                                                p[i]=i+1; 
                                            }
                                            //sorting burst time in ascending order using selection sort
                                            for(i=0;i<n;i++)
                                            {
                                                pos=i;
                                                for(j=i+1;j<n;j++)
                                                {
                                                    if(bt[j]<bt[pos])
                                                        pos=j;
                                                }
                                                tempvar=bt[i];
                                                bt[i]=bt[pos];
                                                bt[pos]=tempvar;
                                                tempvar=p[i];
                                                p[i]=p[pos];
                                                p[pos]=tempvar;
                                            }       
                                            wt[0]=0;            //waiting time for first process will be zero
                                            //calculate waiting time
                                            for(i=1;i<n;i++)
                                            {
                                                wt[i]=0;
                                                for(j=0;j<i;j++)
                                                    wt[i]+=bt[j];
                                                total+=wt[i];
                                            }
                                            avwt=total/n;      //average waiting time
                                            total=0;
                                            Console.WriteLine("\nProcess\t    BT    \tWT\tTT");
                                            for(i=0;i<n;i++)
                                            {
                                                tat[i]=bt[i]+wt[i];     //calculate turnaround time
                                                total+=tat[i];
                                                Console.WriteLine("\nP[" + (i + 1) + "]" + "\t\t" + bt[i] + "\t\t" + wt[i] + "\t\t" + tat[i]);
                                            }
                                            avtat=total/n;     //average turnaround time
                                            Console.WriteLine("\n\nAverage Waiting Time:" + avwt);
                                            Console.WriteLine("\nAverage Turnaround Time:" + avtat);
                                            break;
                                        }
                                    case "3":
                                        {
                                            Console.WriteLine("Enter the total number of processes: ");
                                            temp = Console.ReadLine();
                                            n = Int32.Parse(temp); 
                                            Console.Write("\nEnter Burst Time and Priority: \n");
                                            for (i = 0; i < n; i++)
                                            {
                                                Console.WriteLine("P[" + (i + 1) + "]:");
                                                Console.Write("Burst Time : ");
                                                temp = Console.ReadLine();
                                                bt[i] = Int32.Parse(temp);
                                                Console.Write("Priority : ");
                                                temp = Console.ReadLine();
                                                pr[i] = Int32.Parse(temp);
                                                p[i] = i + 1;           //contains process number
                                            }
                                            //sorting burst time, priority and process number in ascending order using selection sort
                                            for (i = 0; i < n; i++)
                                            {
                                                pos = i;
                                                for (j = i + 1; j < n; j++)
                                                {
                                                    if (pr[j] < pr[pos])
                                                        pos = j;
                                                }
                                                tempvar = pr[i];
                                                pr[i] = pr[pos];
                                                pr[pos] = tempvar;
                                                tempvar = bt[i];
                                                bt[i] = bt[pos];
                                                bt[pos] = tempvar;
                                                tempvar = p[i];
                                                p[i] = p[pos];
                                                p[pos] = tempvar;
                                            }
                                            wt[0] = 0;            //waiting time for first process is zero
                                            //calculate waiting time
                                            for (i = 1; i < n; i++)
                                            {
                                                wt[i] = 0;
                                                for (j = 0; j < i; j++)
                                                    wt[i] += bt[j];
                                                total += wt[i];
                                            }
                                            avwt = total/n;      //average waiting time
                                            total = 0;
                                            Console.WriteLine("\nProcess\t    BT    \tWT\tTT");
                                            for (i = 0; i < n; i++)
                                            {
                                                tat[i] = bt[i] + wt[i];     //calculate turnaround time
                                                total += tat[i];
                                                Console.WriteLine("\nP[" + (i + 1) + "]" + "\t\t" + bt[i] + "\t\t" + wt[i] + "\t\t" + tat[i]);
                                            }
                                            avtat = total / n;     //average turnaround time
                                            Console.WriteLine("\n\nAverage Waiting Time:" + avwt);
                                            Console.WriteLine("\nAverage Turnaround Time:" + avtat);
                                            break;
                                        }
                                    case "4":
                                        {
                                            Console.Write("Enter number of processes: ");
                                            temp = Console.ReadLine();
                                            n = Int32.Parse(temp);
                                            Console.WriteLine("Enter burst time for processes: ");
                                            for(i=0;i<n;i++)
                                            {
                                                Console.Write("P[" + (i + 1) + "]: ");
                                                temp = Console.ReadLine();
                                                bt[i] = Int32.Parse(temp);
                                                st[i]=bt[i];
                                            }
                                            Console.Write("Enter time quantum: ");
                                            temp = Console.ReadLine();
                                            tq = Int32.Parse(temp);
                                            while(true)
                                            {
                                                for(i=0,count=0;i<n;i++)
                                                {
                                                    tempvar=tq;
                                                    if(st[i]==0)
                                                    {
	                                                    count++;
	                                                    continue;
                                                    }
                                                    if(st[i]>tq)
	                                                    st[i]=st[i]-tq;
                                                    else
	                                                    if(st[i]>=0)
	                                                    {
	                                                        tempvar=st[i];
	                                                       st[i]=0;
	                                                    }
	                                                sq=sq+tempvar;
    	                                            tat[i]=sq;
                                                }
                                                if(n==count)
                                                    break;
                                            }
                                            for(i=0;i<n;i++)
                                            {
                                                wt[i]=tat[i]-bt[i];
                                                swt=swt+wt[i];
                                                stat=stat+tat[i];
                                            }
                                            avwt=swt/n;
                                            avtat=stat/n;
                                            Console.WriteLine("Process	   BT	  WT	    TT");
                                            for(i=0;i<n;i++)
                                                Console.WriteLine("P["+ (i+1) + "] 		" + bt[i] + "		" + wt[i] + "		" + tat[i]);
                                            Console.WriteLine("Average Waiting Time: " + avwt);
                                            Console.WriteLine("Average Turnaround Time: " + avtat);
                                            break;
                                        }
                                    case "5":
                                        {
                                            flag = 1;
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("ERROR : Invalid choice! Enter again.");
                                            break;
                                        }
                                }
                                break;
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR: Invalid command! Enter again.");
                            command = null;
                            break;
                        }
                }
            }
        }
    }
}
