- title : Athens F# Meetup on 25/02/2015
- description : F# snippets
- author : Kostas Rontogiannis
- theme : simple
- transition : default

***

## Athens F# Meetup

### F# snippets

_25/02/2015_

***

### About Me

- Kostas Rontogiannis
- [@krontogiannis](https://twitter.com/krontogiannis)
- http://github.com/krontogiannis
- Nessos, NTUA


***

### F# snippets

- (|Active Patterns|)
- Units of Measure<1>
- { Object expressions }

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
    
---

### Parse input

    let input = System.Console.ReadLine()

    match input with
    | Integer i -> printfn "Value is an int %d" i
    | Boolean b -> printfn "Value is a bool %b" b
    | other     -> printfn "Value is string %s" other


---

### Simplify LINQ Expressions

    Expression.OfFunc(fun x -> 0 + x + 32 / 5 - 3 + 0 * x)
    |> simplify

---

### LinqOptimizer [1](https://github.com/nessos/LinqOptimizer/blob/master/src/LinqOptimizer.Core/ExpressionHelpers.fs#L102), [2](https://github.com/nessos/LinqOptimizer/blob/master/src/LinqOptimizer.Core/CSharpExpressionOptimizer.fs#L24)


***

### Units of Measure

Units of measure allow programmers to annotate floats and integers with 
**statically-typed** unit **metadata**. 
By using quantities with units, you enable the **compiler** 
to verify that arithmetic relationships have the correct units, 
which helps **prevent programming errors**.

***

### Object Expressions

An **object expression** is an expression that creates a new instance of an 
**anonymous** object type that is based on an existing 
**base type**, **interface**, or **set of interfaces**.

---
        
    type Person = { Name : string ; Age : int }
    
    let ps =
        [|
            { Name = "Clifford E. May"    ; Age = 32 }
            { Name = "Laura J. Moore"     ; Age = 24 }
            { Name = "Sheryl L. Peterkin" ; Age = 23 }
            { Name = "Clint P. Mitchell"  ; Age = 53 }
            { Name = "William P. Howard"  ; Age = 58 }
            { Name = "Mark R. Ross"       ; Age = 42 }
        |]
    
    System.Array.Sort<T>(ps, ?? : IComparer<T>)


---

    open System.Collections.Generic
    
    type PersonNameComparer () =
        interface IComparer<Person> with
            member this.Compare(x: Person, y: Person) : int = 
                x.Name.CompareTo(y.Name)
    
    type PersonAgeComparer () =
        interface IComparer<Person> with
            member this.Compare(x: Person, y: Person) : int = 
                x.Age.CompareTo(y.Age)

---

    let pnm = new PersonNameComparer()
    System.Array.Sort(ps, pnm)
    
    let pac = new PersonAgeComparer()
    System.Array.Sort(ps, pac)


---

    let nameCmp = { new IComparer<Person> with 
                        member this.Compare(x,y) = x.Name.CompareTo(y.Name) }
    
    let ageCmp = { new IComparer<Person> with 
                        member this.Compare(x,y) = x.Age.CompareTo(y.Age) }

    System.Array.Sort(ps, nameCmp)
    
    System.Array.Sort(ps, ageCmp)


***

#### Slides and samples

https://github.com/krontogiannis/fsharp-meetup-5
http://krontogiannis.github.io/fsharp-meetup-5

***

## Questions?

***

## thnx :)

