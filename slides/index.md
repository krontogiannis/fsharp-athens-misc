- title : Athens F# Meetup on 25/02/2015
- description : F# features
- author : Kostas Rontogiannis
- theme : simple
- transition : default

***

## Athens F# Meetup

25/02/2015

### F# features and snippets

***

### About Me

- Kostas Rontogiannis
- @krontogiannis
- http://github.com/krontogiannis

***

### F# features

- Active Patterns
- Units of Measure
- Object expressions

***

### Active Patterns

_Active Patterns_ allow programmers to wrap **arbitrary values** 
in a union-like data structure for easy **pattern matching**. 
For example, its possible wrap objects with an active pattern, 
so that you can use objects in pattern matching as easily 
as any other **union type**.


---

### Simple

    let (|Even|Odd|) (input : int) =
        if input % 2 = 0 then Even else Odd


---

### Simple

    let (|Even|Odd|) (input : int) =
        if input % 2 = 0 then Even else Odd

    let x = 42

    match x with
    | Even -> printfn "Even"
    | Odd -> printfn "Odd"



---

### Parse input

    let (|Integer|_|) (input : string) =
        match System.Int32.TryParse(input) with
        | true, x -> Some(x)
        | false,_ -> None

    let (|Boolean|_|) (input : string) =
        match System.Boolean.TryParse(input) with
        | true, x -> Some(x)
        | false,_ -> None
    

    let input = System.Console.ReadLine()

    match input with
    | Integer i -> printfn "Value is an int %d" i
    | Boolean b -> printfn "Value is a bool %b" b
    | other     -> printfn "Value is string %s" other


***

### Syntax Highlighting

#### F# (with tooltips)

    let a = 5
    let factorial x = [1..x] |> List.reduce (*)
    let c = factorial a

---

#### C#

    [lang=cs]
    using System;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, world!");
        }
    }

---

#### JavaScript

    [lang=js]
    function copyWithEvaluation(iElem, elem) {
        return function (obj) {
            var newObj = {};
            for (var p in obj) {
                var v = obj[p];
                if (typeof v === "function") {
                    v = v(iElem, elem);
                }
                newObj[p] = v;
            }
            if (!newObj.exactTiming) {
                newObj.delay += exports._libraryDelay;
            }
            return newObj;
        };
    }


---

#### Haskell
 
    [lang=haskell]
    recur_count k = 1 : 1 : zipWith recurAdd (recur_count k) (tail (recur_count k))
            where recurAdd x y = k * x + y

    main = do
      argv <- getArgs
      inputFile <- openFile (head argv) ReadMode
      line <- hGetLine inputFile
      let [n,k] = map read (words line)
      printf "%d\n" ((recur_count k) !! (n-1))

*code from [NashFP/rosalind](https://github.com/NashFP/rosalind/blob/master/mark_wutka%2Bhaskell/FIB/fib_ziplist.hs)*

---

### SQL

    [lang=sql]
    select *
    from
    (select 1 as Id union all select 2 union all select 3) as X
    where Id in (@Ids1, @Ids2, @Ids3)

*sql from [Dapper](https://code.google.com/p/dapper-dot-net/)*

***

**Bayes' Rule in LaTeX**

$ \Pr(A|B)=\frac{\Pr(B|A)\Pr(A)}{\Pr(B|A)\Pr(A)+\Pr(B|\neg A)\Pr(\neg A)} $

***

### The Reality of a Developer's Life 

**When I show my boss that I've fixed a bug:**
  
![When I show my boss that I've fixed a bug](http://www.topito.com/wp-content/uploads/2013/01/code-07.gif)
  
**When your regular expression returns what you expect:**
  
![When your regular expression returns what you expect](http://www.topito.com/wp-content/uploads/2013/01/code-03.gif)
  
*from [The Reality of a Developer's Life - in GIFs, Of Course](http://server.dzone.com/articles/reality-developers-life-gifs)*

***

#### Slides and samples

https://github.com/palladin/StreamsPresentation

***

### Thank you!

***

## Questions?
