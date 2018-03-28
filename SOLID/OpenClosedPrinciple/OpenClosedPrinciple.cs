using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace OpenClosedPrincipleNS
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {

        // PAS BIEN
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> product, Size size)
        {
            foreach (var p in product)
            {
                /* 
                   PropertyInfo SizeProperty = p.GetType().GetProperty("Size", BindingFlags.NonPublic | BindingFlags.Instance);
                   MethodInfo SizeGetter = SizeProperty.GetGetMethod(nonPublic: true);
                   if ((Size)SizeGetter.Invoke(p, null) == size)
                */
                if(p.Size == size)
                    yield return p;
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> product, Color color)
        {
            foreach (var p in product)
            {
                /*
                    PropertyInfo ColorProperty = p.GetType().GetProperty("Color", BindingFlags.NonPublic | BindingFlags.Instance);
                    MethodInfo ColorGetter = ColorProperty.GetGetMethod(nonPublic: true);
                    if ((Color)ColorGetter.Invoke(p, null) == color)
                */
                if (p.Color == color)
                    yield return p;
            }
        }
    }
    // BIEN
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    class OpenClosedPrinciple
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);
            var hummer = new Product("Hummer", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house, hummer };

            var pf = new ProductFilter();
            WriteLine("Green products (old):");
            foreach(var p in pf.FilterByColor(products, Color.Green))
            {
                /*
                    PropertyInfo NameProperty = p.GetType().GetProperty("Name", BindingFlags.NonPublic | BindingFlags.Instance);
                    MethodInfo NameGetter = NameProperty.GetGetMethod(nonPublic: true);
                    var name = (string)NameGetter.Invoke(p, null);
                */
                WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();
            WriteLine("Green products (new):");
            foreach(var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine("Large blue items");
            foreach (var p in bf.Filter(
                products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large)
                )))
            {
                WriteLine($" - {p.Name} is big and blue");
            }

            Read();
        }
    }
}
