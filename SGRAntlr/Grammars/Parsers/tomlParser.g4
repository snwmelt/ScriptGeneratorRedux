parser grammar tomlParser;

options
{
	language=TOML;
	tokenVocab=tomlLexer;
}


document 
	: expression (NL expression)* ;

expression 
	: key_value comment | table comment | comment ;

comment
	: COMMENT? ;

key_value 
	: key '=' value ;

key 
	: simple_key | dotted_key ;

simple_key 
	: quoted_key | unquoted_key ;

unquoted_key 
	: UNQUOTED_KEY ;

quoted_key 
	:  BASIC_STRING | LITERAL_STRING ;

dotted_key 
	: simple_key ('.' simple_key)+ ;

value 
	: string | integer | floating_point | bool | date_time | array | inline_table ;

string 
	: BASIC_STRING | ML_BASIC_STRING | LITERAL_STRING | ML_LITERAL_STRING ;

integer 
	: DEC_INT | HEX_INT | OCT_INT | BIN_INT ;

floating_point 
	: FLOAT | INF | NAN ;

bool 
	: BOOLEAN ;

date_time 
	: OFFSET_DATE_TIME | LOCAL_DATE_TIME | LOCAL_DATE | LOCAL_TIME ;

array 
	: '[' array_values? comment_or_nl ']' ;

array_values 
	: (comment_or_nl value ',' array_values comment_or_nl) | comment_or_nl value ','? ;

comment_or_nl 
	: (COMMENT? NL)* ;

table 
	: standard_table | array_table ;

standard_table 
	: '[' key ']' ;

inline_table 
	: '{' inline_table_keyvals '}' ;

inline_table_keyvals 
	: inline_table_keyvals_non_empty? ;

inline_table_keyvals_non_empty 
	: key '=' value (',' inline_table_keyvals_non_empty)? ;

array_table 
	: '[[' key ']]' ;
