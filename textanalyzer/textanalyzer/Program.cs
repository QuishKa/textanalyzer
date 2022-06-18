using System;
using System.Linq;
using System.Text;

class ReadingError : Exception { }
struct Reader
{
    private static string value;
    private static int pos;
    public Reader(string str)
    {
        value = str;
        pos = 0;
    }
    public char Show()
    {
        return value[pos];
    }
    public void Read()
    {
        ++pos;
    }
}

class Program
{
    static void Main()
    {
        System.Console.OutputEncoding = Encoding.UTF8;
        string input = " ";
        while (input != "exit")
        {
            Console.Clear();
            Console.WriteLine("Подготовил Доронин Вячеслав гр. 0308\n" +
                "Этот анализатор анализирует входную скобочную запись.\n" +
                "Правильная скобочная запись с двумя видами скобок.\n" +
                "Если внутри скобок нет ничего, то должен быть поставлен\n" +
                "по крайней мере один пробел. В других местах пробелов не должно быть.\n\n" +
                "Пример:\n" +
                "Правильная запись: [( )([ ]([ ]( )( )))]\n" +
                "Неправильная запись: [( )()][][()[]]\n\n" +
                "Для проверки скобочной записи введите её\n" +
                "Для выхода из программы введите exit");
            input = Console.ReadLine();
            if (input != "exit")
            {
                if (input.All(x => Array.Exists("[]() ".ToCharArray(), y => x == y)))
                {
                    input += '#';
                    Reader reader = new Reader(input);
                    try
                    {
                        Formula(reader);
                        Console.WriteLine("Скобочная запись верна");
                    }
                    catch (ReadingError)
                    {
                        Console.WriteLine("Скобочная запись неверна");
                    }
                }
                else
                    Console.WriteLine("Ввод содержит символы кроме [ ] ( )");
            }
            Console.WriteLine("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
    static void Formula(Reader reader)
    {
        Expression(reader);
        if (reader.Show() != '#') throw new ReadingError();
    }
    static void Expression(Reader reader)
    {
        if (reader.Show() == '(')
        {
            reader.Read();
            RoundExpression(reader);
        }
        else if (reader.Show() == '[')
        {
            reader.Read();
            SqrExpression(reader);
        }
    }
    static void SqrExpression(Reader reader)
    {
        if (reader.Show() == ' ')
        {
            reader.Read();
        }
        else
        {
            if (reader.Show() == ']') throw new ReadingError();
            Expression(reader);
        }
        if (reader.Show() == ']')
        {
            reader.Read();
        }
        else throw new ReadingError();
        Expression(reader);
    }
    static void RoundExpression(Reader reader)
    {
        if (reader.Show() == ' ')
        {
            reader.Read();
        }
        else
        {
            if (reader.Show() == ')') throw new ReadingError();
            Expression(reader);
        }
        if (reader.Show() == ')')
        {
            reader.Read();
        }
        else throw new ReadingError();
        Expression(reader);
    }
}