﻿using YCSharp;

public class Program
{
    static void Main(string[] args)
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        list.Add(7);
        list.Add(8);
        list.Add(9);
        list.Add(10);

        int i = 0;
        list.For(() =>
        {
            return i > 2;
        }, data =>
        {
            return data > 1;
        }, data =>
        {
            Console.WriteLine(data.element);
            i++;
        });
    }
}