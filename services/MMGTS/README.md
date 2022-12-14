# MMGTS (Match Management Service)

> This stateless service is responsible for handling CRUD operations on Match data


## How does it work?

It shares dapr proto files so services easily can request a rpc call programmatically through InvokeService building block

### ProtoBuf specification

```
syntax = "proto3";

package dapr;

import "google/protobuf/any.proto";
import "google/protobuf/empty.proto";
import "google/protobuf/duration.proto";

option java_outer_classname = "DaprProtos";
option java_package = "io.dapr";

option csharp_namespace = "MMGTS.Server.Protos";

// Dapr definitions
service Dapr {
  rpc PublishEvent(PublishEventEnvelope) returns (google.protobuf.Empty) {}
  rpc InvokeService(InvokeServiceEnvelope) returns (InvokeServiceResponseEnvelope) {}
  rpc InvokeBinding(InvokeBindingEnvelope) returns (google.protobuf.Empty) {}
  rpc GetState(GetStateEnvelope) returns (GetStateResponseEnvelope) {}
  rpc GetSecret(GetSecretEnvelope) returns (GetSecretResponseEnvelope) {}
  rpc SaveState(SaveStateEnvelope) returns (google.protobuf.Empty) {}
  rpc DeleteState(DeleteStateEnvelope) returns (google.protobuf.Empty) {}
}

message InvokeServiceResponseEnvelope {
  google.protobuf.Any data = 1;
  map<string,string> metadata = 2;
}

message DeleteStateEnvelope {
  string storeName = 1;
  string key = 2;
  string etag = 3;
  StateOptions options = 4;
}

message SaveStateEnvelope {
  string storeName = 1;
  repeated StateRequest requests = 2;
}

message GetStateEnvelope {
    string storeName = 1;
    string key = 2;
    string consistency = 3;
}

message GetStateResponseEnvelope {
  google.protobuf.Any data = 1;
  string etag = 2;
}

message GetSecretEnvelope {
  string storeName = 1;
  string key = 2;
  map<string,string> metadata = 3;
}

message GetSecretResponseEnvelope {
  map<string,string> data = 1;
}

message InvokeBindingEnvelope {
  string name = 1;
  google.protobuf.Any data = 2;
  map<string,string> metadata = 3;
}

message InvokeServiceEnvelope {
  string id = 1;
  string method = 2;
  google.protobuf.Any data = 3;
  map<string,string> metadata = 4;
}

message PublishEventEnvelope {
    string topic = 1;
    google.protobuf.Any data = 2;
}

message State {
  string key = 1;
  google.protobuf.Any value = 2;
  string etag = 3;
  map<string,string> metadata = 4;
  StateOptions options = 5;
}

message StateOptions {
  string concurrency = 1;
  string consistency = 2;
  RetryPolicy retryPolicy = 3;
}

message RetryPolicy {
  int32 threshold = 1;
  string pattern = 2;
  google.protobuf.Duration interval = 3;
}

message StateRequest {
  string key = 1;
  google.protobuf.Any value = 2;
  string etag = 3;
  map<string,string> metadata = 4;
  StateOptions options = 5;
}
```


