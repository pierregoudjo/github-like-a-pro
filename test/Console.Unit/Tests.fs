module Tests

open Xunit
open Program
open FsCheck
open FsCheck.Xunit

let multipleOfThree n = n * 3
let multipleOfFive n = n * 5
let multipleOfThreeAndFive n = n * 15
let notMultipleOfThreeOrFive n = (multipleOfThreeAndFive n) - 1

type ThreeGenerator = 
    static member Value = 
        Arb.generate<NonNegativeInt>
        |> Gen.map (fun (NonNegativeInt n) -> multipleOfThree n)
        |> Gen.filter (fun n -> n > 0)
        |> Arb.fromGen

type FiveGenerator = 
    static member Value = 
        Arb.generate<NonNegativeInt>
        |> Gen.map (fun (NonNegativeInt n) -> multipleOfFive n)
        |> Gen.filter (fun n -> n > 0)
        |> Arb.fromGen

type ThreeAndFiveGenerator = 
    static member Value = 
        Arb.generate<NonNegativeInt>
        |> Gen.map (fun (NonNegativeInt n) -> multipleOfThreeAndFive n)
        |> Arb.fromGen

type NotThreeOrFiveGenerator = 
    static member Value = 
        Arb.generate<NonNegativeInt>
        |> Gen.filter (fun (NonNegativeInt n) -> not(n%3=0 || n%5=0) && n> 0)
        |> Gen.map (fun (NonNegativeInt n) -> n )
        |> Arb.fromGen


[<Property(Arbitrary = [| typeof<ThreeGenerator> |])>]
let ``Multiple of three should contain Fizz`` (underTest: int) =
    Assert.Contains("Fizz", fizzbuzz underTest)

[<Property(Arbitrary = [| typeof<FiveGenerator> |])>]
let ``Multiple of five should contain Buzz`` (underTest) =
    Assert.Contains("Buzz", fizzbuzz underTest)

[<Property(Arbitrary = [| typeof<ThreeAndFiveGenerator> |])>]
let ``Multiple of three and five should be equal to FizzBuzz`` (underTest) =
    Assert.Equal("FizzBuzz", fizzbuzz underTest)

[<Property(Arbitrary = [| typeof<NotThreeOrFiveGenerator> |])>]
let ``Not Multiple of three or five should be equal to the same number`` (underTest) =
    Assert.Equal(string underTest, fizzbuzz underTest)


