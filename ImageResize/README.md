# Image resize API - coding challenge

Image resize API is being written for the atom coding challenge and aims to fulfil all the goals stated below.

## Goals of the challenge

We would like you to build a system, using whichever language, framework, runtime etc. you choose, that will accept a GET request for an image provided in the linked zip file, and return the image to the requester.

The GET request should allow the receipt of the following parameters, and the returned image should reflect those parameters:

- Resolution
- Background colour (optional)
- Watermark (an optional string provided as part of the GET request)
- Image file type (png, jpg etc.)

As well as processing and returning the image, the system should cache it’s results, so that if a subsequent request is made for the same image, with the same parameters, the system does not need to reprocess the base image, it instead uses the existing image in the cache.

The system should be designed with scalability in mind.  While you have been provided with a zip file of 500 images, and as part of testing we will only be requesting single images at a time, consideration should be given for the fact that our production systems carry some 40,000 images, and receive multiple image requests per second.