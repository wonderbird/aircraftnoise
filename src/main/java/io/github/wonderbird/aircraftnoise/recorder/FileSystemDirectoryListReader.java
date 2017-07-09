package io.github.wonderbird.aircraftnoise.recorder;

import java.io.File;

public class FileSystemDirectoryListReader implements DirectoryListReader {
    public DirectoryList read() {
        File dir = new File(".");
        DirectoryList dirList = new DirectoryList();

        File[] files = dir.listFiles();

        for (File file : files) {
            if (file.isFile()) {
                dirList.addFile("F: " + file.getPath());
            } else {
                dirList.addFile("D: " + file.getPath());
            }
        }

        return dirList;
    }
}
