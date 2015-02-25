

/// Employee type. Salary either in EUR or USD.
type Employee = { Name : string ; Salary : float }



let europeEmpl =
    [
        { Name = "Clifford E. May"    ; Salary = 38900.4 }
        { Name = "Laura J. Moore"     ; Salary = 35900.6 }
        { Name = "Sheryl L. Peterkin" ; Salary = 38990.0 }
    ]





let usaEmpl =
    [
        { Name = "Clint P. Mitchell"  ; Salary = 30843.1 }
        { Name = "William P. Howard"  ; Salary = 32247.9 }
        { Name = "Mark R. Ross"       ; Salary = 40941.3 }
        { Name = "John L. Peterkin"   ; Salary = 50990.0 }
    ]





let total = 
    europeEmpl 
    |> List.append usaEmpl
    |> List.sumBy(fun empl -> empl.Salary)  
  
  

  
  
  
  
  























  
  
  
  
  
  
    
type Employee< [<Measure>]'T > = { Name : string ; Salary : float<'T> }


[<Measure>]type USD
[<Measure>]type EUR

let rate = 0.7<USD/EUR>

float rate

let EURtoUSD (eur : float<EUR>) = rate * eur
let USDtoEUR (usd : float<USD>) = usd / rate

let europeEmpl =
    [
        { Name = "Clifford E. May"    ; Salary = 38900.4<EUR> }
        { Name = "Laura J. Moore"     ; Salary = 35900.6<EUR> }
        { Name = "Sheryl L. Peterkin" ; Salary = 38990.0<EUR> }
    ]


let usaEmpl =
    [
        { Name = "Clint P. Mitchell"  ; Salary = 30843.1<USD> }
        { Name = "William P. Howard"  ; Salary = 32247.9<USD> }
        { Name = "Mark R. Ross"       ; Salary = 40941.3<USD> }
    ]

  
let total = 
    europeEmpl 
    |> List.map (fun e -> { Name = e.Name; Salary = EURtoUSD e.Salary})
    |> List.append usaEmpl
    |> List.sumBy(fun empl -> empl.Salary)  
  