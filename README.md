# CustomProgrammingLanguage

Semestral work for Programming Language Theory. The application is used for parsing my own programming language called V++.

# What I Learned

* EBNF syntax
* Basic principles of how programming languages are created.

# V++ Syntax Test
```
integer myVariableFive;
string appendedString = "test of appending" + " string and continue" + " later";
integer counter = 0;

recursiveMethod(integer firstIntegerParameter, string secondStringParameter)
{
	println(secondStringParameter);
	println(firstIntegerParameter);
	counter = counter + 1;
	if(counter <= 3)
	{
		run recursiveMethod(counter, "Counter value:");
	}
}
program()
{
	myVariableFive = 5;
	forcycle(integer i = 0, i < 2, i++)
	{
		forcycle(integer j = 0, j < 2, j++)
			{
				forcycle(integer z = 0, z < 2, z++)
				{
					println("Third nested loop!");
				}
				println("Second nested loop!");
			}
		println("First loop!");
	}
	if(counter + 2 < 4)
	{
		println("Yahoo, condition is true!");
	}
	else
	{
		println("Ouch, condition is false");
	}
	if(6 > 5)
	{
		println("6 is indeed higher than 5!");
		if(6 > 4)
		{
			println("And also, 6 is higher than 4, NESTED CONDITION!");
		
			forcycle(integer i = 0, i < 3, i++)
			{
				println("You got through 2 conditions, congratulations!");
			}
		}
	}
	println(appendedString);
	println("Test of order operations, 5 + 5 * 8 - 6 is:");
	println(5 + 5 * 8 - 6);
	run recursiveMethod(50, "Hello from recursive method!");
	run PrintMyFriends("John", "Mona", "Lisa", "Denver", "Detroit");
}

PrintMyFriends(string p1, string p2, string p3, string p4, string p5)
{
	println("My friends are:");
	println(p1);
	println(p2);
	println(p3);
	println(p4);
	println(p5);
}
```
# EBNF representation of V++
```
grammar V++;

program :=
    {var_definition},
    "program()
    "{",
	{statement},
	{method_call},
    "}"
;

function_call :=
    "run", 
    ("println", "(", {(identifier | arithmetic_statement) | string)}, ")")
    ),
    ";"
;

statement :=
    (var_definition, ";")
    | logic_statement
    | loop_statement
    | (method_call, ";")
    | "{" logic statement "}"
;

logic_statement :=
    "if", paren_expr, statement, [("else"), paren_expr, statement)], "else", statement
;

paren_expr :=
    "(" boolean_expr ")"
;

loop_statement :=
    "forcycle", "(" {integer, identifier, "=", integer "," idientifier, expr, integer, ("++" | "--" )")",
    "{"
	{statement}
    "}"
;

boolean_expr :=
    expr
    | (identifier | boolean_expr), ("==" | "!="), (boolean_expr | identifier)
;

expr:=
    sum, "<", sum
    | sum, ">", sum
    | sum, "<=", sum
    | sum, ">=", sum
    | sum, "==", sum
    | sum, "!=", sum
;
sum :=
    arithmetic_term
    | sum, "+" term
    | sum, "-" term
;

arithmetic_term :=
    factor, {("*" | "/"), factor}
;

factor :=
    identifier
    | number
;

var_definition :=
    data_type, identifier,
    [("=", string | integer)],
    ";"
;

identifier := 
    /[a-zA-Z$_][a-zA-Z0-9$_]*/
;

integer :=
    ["-"], digit, [{digit}], ["." , {digit}];
;

digit := {"0"|"1"|"2"|"3"|"4"|"5"|"6"|"7"|"8"|"9"};

string :=
    '"', /[a-zA-Z0-9$_]*/, '"'
;
```
