﻿using Charity.Network.ObjectProtocol;
using Charity.Service.Observer;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
#pragma warning disable SYSLIB0011

namespace Charity.Network.Client
{
public abstract class ServiceObjectProxyBase
    {
        private string Host;
        private int Port;

        protected IObserver Client;

        private NetworkStream Stream;
        private IFormatter Formatter;
        private TcpClient Connection;

        private Queue<IResponse> Responses = new Queue<IResponse>();
        private volatile bool IsFinished = false;

        private EventWaitHandle WaitHandle;


        public ServiceObjectProxyBase(string host, int port)
        {
            Host = host;
            Port = port;
        }
        
        protected abstract void HandleUpdate(UpdateResponse update);

        protected void CloseConnection()
        {
            IsFinished = true;
            try
            {
                Stream.Close();
                Connection.Close();
                WaitHandle.Close();
                Client = null;
            }
            catch (Exception e)
            {
                throw new ProxyException(e);
            }
        }

        protected void SendRequest(IRequest request)
        {
            try
            {
                Formatter.Serialize(Stream, request);
                Stream.Flush();
            }
            catch (Exception e)
            {
                throw new ProxyException("Error sending object " + e);
            }
        }

        protected IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                WaitHandle.WaitOne();
                lock (Responses) 
                {                    
                    response = Responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                throw new ProxyException(e);                
            }
            return response;
        }

        protected void InitializeConnection()
        {
            try
            {
                Connection = new TcpClient(Host, Port);
                Stream = Connection.GetStream();
                Formatter = new BinaryFormatter();
                IsFinished = false;
                WaitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                throw new ProxyException(e);
            }
        }

        private void StartReader()
        {
            new Thread(Run).Start();            
        }
        

        public virtual void Run()
        {
            while (!IsFinished) 
            {
                try
                {
                    object response = Formatter.Deserialize(Stream);
                    Console.WriteLine($"Response Received : {response}");
                    if (response is UpdateResponse)
                    {              
                        Console.WriteLine($"Received UpdateResponse : {response}");
                        HandleUpdate(response as UpdateResponse);
                    }
                    else
                    {
                        lock (Responses)
                            Responses.Enqueue(response as IResponse);                        
                        WaitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    ExceptionThrown?.Invoke(e);
                    //throw new ProxyException(e);                    
                }
            }
        }

        public delegate void OnExceptionThrown(Exception e);
        public event OnExceptionThrown ExceptionThrown;

        protected void TestConnectionOpen()
        {
            if (Connection == null)
                throw new ProxyException("Connection is not open");            
        }

    }
}