# The Sequence Programming Language

This is a programming language developed mostly for fun, as well as to teach basic text-based programming. It uses a mostly familiar syntax to those who have programmed in any other programming language. It is a modified version of CustomProgrammingLanguage.

# Getting Set up
Right now, Sequence is only avaliable on macOS in this repo, but Windows support is coming. You can just build the .sln on Windows, and it will generate a useable executable.
I recommend addint the seq executable to PATH, as you can learn how at this link: https://wpbeaches.com/how-to-add-to-the-shell-path-in-macos-using-terminal/ Add the /netcoreapp3.1/ folder for the location.

# Using
To execute Sequence in terminal, navigate to the directory containing Sequence, and type `./seqc /path/to/file.seq`. (Or run `seqc /path/to/file.seq` if it is in the PATH.)

IMPORTANT: Sequence only reads .seq files in plain-text. Make sure your extension is .seq!

# Basics
Every .seq file must include a main method, as with many other programming languages. An example is as follows:

```
program()
{
	println("Hello world!");	
	#! Comment!
}
```

In this case `program(){}` is like the `main()` method in many _****many****_ programming languages.

# Syntax

#### Keywords

`#!`: Comment. Acts like any other comment.

`print`: Prints text with no newline. `print("Hello!");`

`println`: Print text with newline. Can print text `println("hello");` as well as `println(myvariable);` and `println("text" + "text2"`, as well as math: `println(5 + 5 * 8 - 6);`

`forcycle`: Operates as your typical `foreach` statement.
```
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
```
`if-else`: Operates like your average `if-else` statment.
```
if(counter + 2 < 4)
	{
		println("Yahoo, condition is true!");
	}
	else
	{
		println("Ouch, condition is false");
	}
```

`input`: Obtains input from the console. Cannot be used to get integer input.
```
program()
{
	println("Hello world!");	
	#! Comment!
	string myName = "";
	println("Enter your name: ");
	input = myName;
	print("Welecome, ");
	print(myName);
	print(".");
}
```

`randint`: Creates a random integer.
Syntax:
```
program()
{

	integer randomNumber = 4 #! The parser takes the value as the highest number to create to.
	randint = randomNumber;
	println(randomNumber);
}
```

`play`: Plays specified audio path.
Syntax:
```
program()
{

	string path = "/Users/user/Music/EpicTheme.mp3";
	play = path;
	
}

```


#### Variables

`integer`: Works like this: `integer myInt = 0;` Can be modified. E.G.

```
#! This is the integer to modify.
integer counter = 0;


program()
{
	#! Modification is here:
	if(counter + 2 < 4)
	{
		println("Yahoo, condition is true!");
	}
	else
	{
		println("Ouch, condition is false");
	}
	
}
```

`string`: Declaration: `string myString = "content";`. Strings can be modified: 

```
program()
{
    string myName = "Bob";
    string newName = myName + " , hi!";
    println(newName);
}
```

#### Functions

You can declare custom functions using basic syntax like so:

```
#! Functions can take integers as well.
printNames(string n1, string n2, string n3, string n4, string n5)
{
	println("Names:");
	println(n1);
	println(n2);
	println(n3);
	println(n4);
	println(n5);
}

```

And then call them like:

`run printNames("Name1", "Name2", "Name3", "Name4", "Name5");`

This line can be run anywhere, even inside other functions.
