CC=gcc
CFLAGS=-I.
DEPS = ./pt
OBJ = *.o
CFILES := prototest.c


%.o: $(CFILES) $(DEPS)
	$(CC) -c -o $@ $< $(CFLAGS)

all: $(OBJ)
	$(CC) -o $@ $^ $(CFLAGS)