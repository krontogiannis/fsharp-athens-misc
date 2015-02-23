

// Simple

let (|Even|Odd|) (input : int) =
    if input % 2 = 0 then Even else Odd

let x = 42

match x with
| Even -> printfn "Even"
| Odd -> printfn "Odd"


// read input

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


