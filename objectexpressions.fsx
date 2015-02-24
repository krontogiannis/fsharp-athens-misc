

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

System.Array.Sort<T>(T[], IComparer<T>)

open System.Collections.Generic

type PersonNameComparer () =
    interface IComparer<Person> with
        member this.Compare(x: Person, y: Person) : int = 
            x.Name.CompareTo(y.Name)

type PersonAgeComparer () =
    interface IComparer<Person> with
        member this.Compare(x: Person, y: Person) : int = 
            x.Age.CompareTo(y.Age)

let pnm = new PersonNameComparer()
System.Array.Sort(ps, pnm)

let pac = new PersonAgeComparer()
System.Array.Sort(ps, pac)


let nameCmp = { new IComparer<Person> with 
                    member this.Compare(x,y) = x.Name.CompareTo(y.Name) }

let ageCmp = { new IComparer<Person> with 
                    member this.Compare(x,y) = x.Age.CompareTo(y.Age) }


System.Array.Sort(ps, nameCmp)

System.Array.Sort(ps, ageCmp)

ps |> Seq.iter (printfn "%A")
