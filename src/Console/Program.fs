module Program

// // For more information see https://aka.ms/fsharp-console-apps
// printfn "Hello from F#"

let fizzbuzz number = 
  match number with
  | x when x%3=0 && x%5=0 -> "FizzBuzz"
  | x when x%3=0 -> "Fizz"
  | x when x%5=0 -> "Buzz"
  | x -> string x

[<EntryPoint>]
let main args =
  let number = int args[0]
  [0 .. number] 
  |> Seq.map fizzbuzz
  |> Seq.iter (printfn "%s")
  0



