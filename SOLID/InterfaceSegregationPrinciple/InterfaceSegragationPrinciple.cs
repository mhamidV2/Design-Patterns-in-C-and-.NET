using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace InterfaceSegregationPrincipleNS
{
    public class Document
    {

    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            //throw new NotImplementedException();
        }
    }

    public class OldFashionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            //throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter //...
    {
        //
    }

    public class MultiFictionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFictionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }// decorator pattern
    }

    class InterfaceSegragationPrinciple
    {
        static void Main(string[] args)
        {
        }
    }
}
