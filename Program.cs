﻿using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System;


namespace Zachary_Renyhart_Assignment_6._3._1
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            //This is creating the random generator
            Random random = new();
            CallCenter center = new();
            center.Call(1234);
            center.Call(5678);
            center.Call(1468);
            center.Call(9641);
            //While loop means while this is true, this loop will perform
            while (center.AreWaitingCalls())
            {
                IncomingCall call = center.Answer("Marcin")!;
                Log($"Call #{call.Id} from client #{call.ClientId} answered by {call.Consultant}.");

                //This is a delay timer that will randomly answer the calls 
               await Task.Delay(random.Next(1000, 10000));
                center.End(call);
                Log($"Call #{call.Id} from client #{call.ClientId} ended by {call.Consultant}.");
            }
            //This displays the time and date the call started and ended!
            void Log(string text) =>
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {text}");
        }

    }

    public class IncomingCall
        //This is the class Incoming call with the properties below
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CallTime { get; set; }
        public DateTime? AnswerTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Consultant { get; set; }
    }

    public class CallCenter
    {
        private int _counter = 0;
        public Queue<IncomingCall> Calls { get; private set; }
        public CallCenter() =>
            Calls = new Queue<IncomingCall>();

        public IncomingCall Call(int clientId)
        {
            IncomingCall call = new()
            {
                Id = ++_counter,
                ClientId = clientId,
                CallTime = DateTime.Now
            };
            Calls.Enqueue(call);
            return call;
        }

        public IncomingCall? Answer(string consultant)
        {
            if (!AreWaitingCalls()) { return null; }
            IncomingCall call = Calls.Dequeue();
            call.Consultant = consultant;
            call.AnswerTime = DateTime.Now;
            return call;
        }

        public void End(IncomingCall call)
            => call.EndTime = DateTime.Now;

        public bool AreWaitingCalls() => Calls.Count > 0;

        
        
    }
    
}
