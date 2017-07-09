package io.github.wonderbird.aircraftnoise.recorder.logic;

import io.github.wonderbird.aircraftnoise.recorder.logic.interfaces.DirectoryListReader;
import io.github.wonderbird.aircraftnoise.recorder.models.DirectoryList;

public class FakeDirectoryListReader implements DirectoryListReader {
    public DirectoryList read() {
        DirectoryList list = new DirectoryList();

        list.addFile("File 1.txt");
        list.addFile("File 2.txt");
        list.addFile("File 3.txt");
        list.addFile("File 4.txt");

        return list;
    }
}
