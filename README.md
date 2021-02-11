# IaC tooling

This document explores different IaC tools and their characteristics on general level and then takes a small dive on the AWS Cloud Development Kit (CDK) functionality.

## Types of IaC tools

I would split the IaC tools into couple of very rough categories.

First split is done according to their target groups

* Cloud-specific tools
  * AWS CloudFormation
  * AWS Cloud Development Kit (CDK)
  * Azure Resource Manager
  * Google Cloud Deployment Manager
* Cloud-agnostic tools
  * Terraform
  * Pulumi
  * and many many more

Next division is by the type of language used by the tool,

* Real programming language
  * AWS Cloud Development Kit (CDK )
  * Pulumi
* Domain specific language
  * AWS CloudFormation (yaml/json)
  * Azure Resource Manager (json)
  * Google Cloud Deployment Manager (yaml)
  * Terraform (hcl)

