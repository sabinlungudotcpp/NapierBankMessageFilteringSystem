# Napier Bank Message Filtering System Service
A message filtering system that validates, sanitises and categorises SMS, Email and Tweet messages from end-users. URL's contained in emails will be written to a quarantine list to quarantine URLs. SMS messages may contain abbreviations and they are fully expanded into their own form.

Napier Bank is a message filtering system that is used to handle incom-ing  messages from a wide range variety of users accessing the system. The system is required to process different types of messages which willbe validated, sanitised and categorised based on the message type.

Before  any  code  will  be  written  for  the  system,  we  are  required  to undertake  the  following  tasks  and  the  report  is  required to include a good amount of detail on:

1.  Gathering the requirements for the system that will convey thefunctional and non-functional requirements of the system.

2.  Converting the requirements of the system into a coherent de-sign that will include a class diagram to show the relationships ofthe  classes, attributes and  methods of the system  as  well  as  pro-ducing a sequence diagram to show the interaction of the system.

3.  Plan and create a test plan that will pinpoint different featuresof the system that should be tested in order for it to pass the testssuccessfully.

4.  Create a version control plan.

5.  Produce  an  evolution  strategy  for  the  Napier  Bank  Message Filtering System.

# Requirements Specification

The  Requirements gathering is a very important stage of the software engineering life cycle due to the fact that the engineers that are workingas part of a team, usually most of the time in a SCRUM team which is a  methodology of agile development need to know the different types of features the software will have as well as any constraints that will hinder the performance of the system.  

There are two specific types of requirements:

User Requirements: Statements in natural language that are typically written for customers.
System Requirements: Structured sentences that highlight the system’s functionality.

The requirements of the Napier Bank Messaging System will be complete which means that the requirements will contain detailed descrip-tions of the features required.

Sometimes  the  end-users of the system may not be fully certain on which features they would like the system to have and therefore a requirements analysis is performed to find out what features users would like to see and then they are converted into a requirements specification. 

There are two types of requirements:

Functional Requirements and Non-Functional Requirements.

There are also five various stages to requirements engineering in a project, each of which have their own purpose:

1.  Requirements Elicitation

2.  Requirements Analysis

3.  Requirements Specification

4.  Requirements Validation

5.  Requirements Management

Firstly, the Requirements Elicitation stage is where the developers workalongside with stakeholders to find out what features they would like from the system as  well as identifying and establishing a clear scope and boundaries for the project and any conflicts and there are a couple of techniques for this:

Interviewing Techniques

Questionnaires

Collaborative Sessions

After  the  requirements  elicitation  stage  is  successfully  complete,  theRequirements  Analysis  stage  begins  where  a  deeper  understanding  ofthe system and its interactions is made.  Furthermore, the Requirements Specification document is written after a coherent analysis is performed.

The requirements specification document is a type of document that captures the system and software requirements in order to support their systematic review. It is a document that describes the system to be developed in a way that can be revised and evaluated.

# Napier Bank Message Filtering Service - Functional Requirements

The functional requirements are the type of requirements that describes what features the system should provide, how the system should react and behave to  particular user inputs and also sometimes it may state what the system should NOT do. These are simply just high-level statements of what the system will do. 

The functional requirements of the Napier Bank Message Filtering Service are as follows:

**1. Read JSON or TXT file  containing  different  types  of  messages  oridentify manual message inputs by the end-users.**

**2. Validate  the  messages  inputted  from  the  file or manually by the end-users and pinpoint which class of messages they fall under:**

SMS Messages

Email Messages

Tweet Messages

3. Sanitise (process) the messages in the following forms:

**3.1 Find text abbreviations in the messages and add their defini-tion after them in the full form.**

**3.2 Remove URLs contained in end-user messages and write them to a quarantine list.**

**3.3 Replace URLs with URL Quarantined in the body of the message.**

**3.4 Identify the nature of incident, associate it to a sort code and write to a SIR list.**

**3.5 Identify hashtags which represent tweets and count the number of uses to produce a trending list with the number of hashtags included in the tweet message.**

**3.6 Add the twitter IDs and mentions to a mentions list denoted and starting with an @ symbol.**

**3.7 Categorise the types of messages - SMS, Email or Tweet.**

**3.8 Write processed messages to a JSON file.**
