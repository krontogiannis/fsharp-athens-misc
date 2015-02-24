

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



// linq expressions

open System
open System.Linq.Expressions


type Expression with
    static member GetBody(func : Expression<Func<'T,'U>>) = func.Body


let (|Int|_|) (input : Expression) =
    match input with
    | :? ConstantExpression as e when e.Type = typeof<int> -> Some(e.Value :?> int)
    | _ -> None

let (|Var|_|) (input : Expression) =
    match input with
    | :? ParameterExpression as e -> Some(e.Name)
    | _ -> None

let (|Bin|_|) (input : Expression) =
    match input with
    | :? BinaryExpression as e -> Some(e.Left, e.Right, e.NodeType)
    | _ -> None

let (|Add|Sub|Mul|Div|) (input : Expression) =
    match input with
    | Bin(l,r, ExpressionType.Add)      -> Add(l, r)
    | Bin(l,r, ExpressionType.Subtract) -> Sub(l, r)
    | Bin(l,r, ExpressionType.Multiply) -> Mul(l, r)
    | Bin(l,r, ExpressionType.Divide)   -> Div(l, r)
    | _                                  -> failwith "Invalid expression"

let rec simplify (expr : Expression) =
    match expr with
    | Var _ 
    | Int _ -> expr

    | Add(Int x, Int y) -> Expression.Constant(x + y) :> _
    | Sub(Int x, Int y) -> Expression.Constant(x - y) :> _
    | Mul(Int x, Int y) -> Expression.Constant(x * y) :> _
    | Div(Int x, Int y) -> Expression.Constant(x / y) :> _

    | Add(Int 0, e)
    | Add(e, Int 0) -> simplify e

    | Mul(Int 0, e)
    | Mul(e, Int 0) -> Expression.Constant(0) :> _

    | Bin(l, r, t)  -> 
        let l' = simplify l
        let r' = simplify r
        if l = l' && r = r' then expr else Expression.MakeBinary(t, l', r') :> _

    | _ -> failwith "Invalid expression"

let rec fix opt x =
    let x' = opt x
    if x = x' then x' else opt x'

Expression.GetBody(fun x -> -10 + 54 - 2)
|> fix simplify

Expression.GetBody(fun x -> x * 0)
|> fix simplify

Expression.GetBody(fun x -> 0 + x + 32 / 5 - 3 + 0 * x )      
|> fix simplify




//#region System.Type
let (|Named|Array|Ptr|Param|) (typ : System.Type) =
    if typ.IsGenericType
    then Named(typ.GetGenericTypeDefinition(), typ.GetGenericArguments())
    elif typ.IsGenericParameter then Param(typ.GenericParameterPosition)
    elif not typ.HasElementType then Named(typ, [||])
    elif typ.IsArray then Array(typ.GetElementType())
    elif typ.IsByRef then Ptr(true, typ.GetElementType())
    elif typ.IsPointer then Ptr(false, typ.GetElementType())
    else failwith "MSDN says this can't happen"

let rec formatType typ =
    match typ with
    | Named (con, [||]) -> sprintf "%s" con.Name
    | Named (con, args) -> sprintf "%s<%s>" con.Name (formatTypes args)
    | Array (arg) -> sprintf "%s []" (formatType arg)
    | Ptr(true, arg) -> sprintf "%s&" (formatType arg)
    | Ptr(false, arg) -> sprintf "%s*" (formatType arg)
    | Param(pos) -> sprintf "!%d" pos

and formatTypes typs =
        String.Join(",", Array.map formatType typs)



let t = (1, [1..10], [|"foo"|])


t.GetType().FullName

formatType <| t.GetType()
//#endregion